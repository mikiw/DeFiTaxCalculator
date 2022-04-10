using DeathCryptoAndTaxes.Flows;
using DeathCryptoAndTaxes.Helpers;
using DeathCryptoAndTaxes.HttpHandlers;
using DeathCryptoAndTaxes.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DeathCryptoAndTaxes
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Death Crypto And Taxes, only sure things to come in the future! Still not sure about death...");

            var config = new Configs()
            {
                EtherscanTxListUrl = ConfigurationManager.AppSettings["EtherscanTxListUrl"],
                EtherscanGetAbitUrl = ConfigurationManager.AppSettings["EtherscanGetAbitUrl"],
                EtherscanApiKey = ConfigurationManager.AppSettings["EtherscanApiKey"],
            };

            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton(config)
                .AddTransient<IEtherscanApiHandler, EtherscanApiHandler>()
                .AddTransient<IEthereumTaxFlow, EthereumTaxFlow>()
                .BuildServiceProvider();

            var walletsToProcess = new List<WalletNetwork>();
            walletsToProcess.Add(new WalletNetwork() { Wallet = "0x299f770d90334c11f6ae65d770a7ce733f1154d7", Network = Network.Ethereum });

            foreach (var wallet in walletsToProcess)
            {
                switch (wallet.Network)
                {
                    case Network.Ethereum:
                        var service = serviceProvider.GetService<IEthereumTaxFlow>();
                        var results = service.GetTaxEventsFromWalletAsync(wallet.Wallet).Result;
                        ViewHelper.Display(results);

                        break;
                    default:
                        Console.WriteLine("Network not found.");
                        break;
                }
            }

            Console.ReadLine();
        }
    }
}
