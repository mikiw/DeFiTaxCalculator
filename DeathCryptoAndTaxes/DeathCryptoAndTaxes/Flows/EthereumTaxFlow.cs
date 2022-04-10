using DeathCryptoAndTaxes.Helpers;
using DeathCryptoAndTaxes.HttpHandlers;
using DeathCryptoAndTaxes.Models;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts;
using Nethereum.Contracts.Services;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace DeathCryptoAndTaxes.Flows
{
    public class EthereumTaxFlow : IEthereumTaxFlow
    {
        private readonly IEtherscanApiHandler _ethereumTransactionHandler;

        public EthereumTaxFlow(IEtherscanApiHandler authenticationHandler)
        {
            _ethereumTransactionHandler = authenticationHandler;
        }

        public async Task<IEnumerable<EthereumTxTaxEvent>> GetTaxEventsFromWalletAsync(string wallet)
        {
            var taxEvents = new List<EthereumTxTaxEvent>();

            try
            {
                // Get all valid transactions
                var transactions = await _ethereumTransactionHandler.GetTransactionsAsync(wallet);
                var allValidtransactions = transactions.Where(x => x.IsError == "0").ToList();

                // Get all unique contract addresses and contracts ABIs
                var contractInteractions = allValidtransactions.Where(x => x.Input != "0x");
                var uniqueContractsAddresses = new HashSet<string>(contractInteractions.Select(x => x.To));
                var contracts = await GetContractsAsync(uniqueContractsAddresses);

                // Process transactions to get tax events
                foreach (var transaction in allValidtransactions)
                {
                    taxEvents.Add(ProcessTransaction(transaction, contracts));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taxEvents;
        }

        private EthereumTxTaxEvent ProcessTransaction(EthereumTx transaction, IEnumerable<EthereumContract> contracts)
        {
            var taxEvent = new EthereumTxTaxEvent();

            taxEvent.TxId = transaction.Hash;
            taxEvent.To = transaction.To;
            taxEvent.From = transaction.From;
            taxEvent.Amount = Web3.Convert.FromWei(BigInteger.Parse(transaction.Value));
            taxEvent.GasUsed = transaction.GasUsed;
            taxEvent.TimeStamp = transaction.TimeStamp;

            if (transaction.Input == "0x")
            {
                // Eth transactions
                taxEvent.Type = "ETH transfer";
            }
            else if (transaction.Input != "0x")
            {
                // Eth contract interaction
                taxEvent.Type = "Contract interaction";

                var contract = contracts.First(x => x.Contract.Address == transaction.To);
                var functionSignature = transaction.Input.Substring(2, 8); // Tak first 4 bytes of function signature
                var findSignature = contract.Signatures.FirstOrDefault(x => x.Signature == functionSignature);

                if (findSignature != null)
                {
                    var function = contract.Contract.GetFunctionBySignature(functionSignature);
                    var decodedInput = function.DecodeInput(transaction.Input);

                    if (contract.Protocol == Protocol.UniswapV2)
                    {
                        UpdateTaxEventForUniswapV2(taxEvent, decodedInput, findSignature.FunctionName);
                    }
                    else
                    {
                        taxEvent.ContractFunction = $"{findSignature.FunctionName} (Unsupported)";
                        taxEvent.Protocol = "Other protocol";
                    }
                }
                else
                {
                    taxEvent.ContractFunction = "(Error) Unreadable contract"; // In case of a buggy contract
                }
            }
            else
            {
                // Eth unsupported type interaction
                taxEvent.Type = "Unsupported Type";
            }

            return taxEvent;
        }

        private void UpdateTaxEventForUniswapV2(EthereumTxTaxEvent taxEvent, List<ParameterOutput> decodedInput, string functionName)
        {
            taxEvent.Protocol = "Uniswap (v2)";

            if (functionName.StartsWith("addLiquidity"))
            {
                taxEvent.ContractFunction = $"{functionName} {UniswapV2DecoderHelper.AddLiquidity(decodedInput)}";
            }
            else if (functionName.StartsWith("removeLiquidity"))
            {
                taxEvent.ContractFunction = $"{functionName} {UniswapV2DecoderHelper.RemoveLiquidity(decodedInput)}";
            }
            else if (functionName.StartsWith("swapExactETHForTokens"))
            {
                taxEvent.ContractFunction = $"{functionName} {UniswapV2DecoderHelper.SwapExactEthForTokens(decodedInput)}";
            }
            else if (functionName.StartsWith("swapExactTokensForETH"))
            {
                taxEvent.ContractFunction = $"{functionName} {UniswapV2DecoderHelper.SwapExactTokensForEth(decodedInput)}";
            }
            else if (functionName.StartsWith("swapExactTokensForTokens"))
            {
                taxEvent.ContractFunction = $"{functionName} {UniswapV2DecoderHelper.SwapExactTokensForTokens(decodedInput)}";
            }
            else
            {
                taxEvent.ContractFunction = $"{functionName}";
            }
        }

        private async Task<IEnumerable<EthereumContract>> GetContractsAsync(HashSet<string> uniqueContractsAddresses)
        {
            var tasks = new List<Task>();
            foreach (var address in uniqueContractsAddresses)
            {
                tasks.Add(GetContractAbiWithSignaturesAsync(address));
            }

            // Run the tasks in parallel, and wait until all have been run
            await Task.WhenAll(tasks);

            var results = new List<EthereumContract>();
            foreach (var task in tasks)
            {
                results.Add(((Task<EthereumContract>)task).Result);
            }

            return results;
        }

        private async Task<EthereumContract> GetContractAbiWithSignaturesAsync(string address)
        {
            try
            {
                Thread.Sleep(500); // just to avoid "Max rate limit reached" etherscan - Could be done differently...
                var abi = await _ethereumTransactionHandler.GetContractAbiAsync(address);

                return new EthereumContract()
                {
                    Contract = new Contract(new EthApiContractService(null), abi, address),
                    Signatures = AbiHelper.GenerateFunctionSignatures(abi),
                    Protocol = UniswapV2MainnetHelper.GetAddresses().Contains(address) ? Protocol.UniswapV2 : Protocol.Other
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
