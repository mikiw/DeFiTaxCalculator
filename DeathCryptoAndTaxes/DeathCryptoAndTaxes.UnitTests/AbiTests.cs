using DeathCryptoAndTaxes.Helpers;
using Nethereum.Contracts;
using Nethereum.Contracts.Services;
using NUnit.Framework;
using System.Linq;

namespace DeathCryptoAndTaxes.UnitTests
{
    [TestFixture]
    public class Abi_ContractSignatures
    {
        private string _abi;

        [SetUp]
        public void SetUp()
        {
            _abi = System.IO.File.ReadAllText(@"TestData\Abi-0x7a250d5630b4cf539739df2c5dacb4c659f2488d.json");
        }

        [Test]
        public void IsSwapExactTokensForEth()
        {
            var signatures = AbiHelper.GenerateFunctionSignatures(_abi);
            var input = System.IO.File.ReadAllText(@"TestData\InputSwapExactTokensForEth.txt");

            var signature = input.Substring(2, 8);
            var result = signatures.First(x => x.Signature == signature);

            Assert.AreEqual(result.FunctionName, "swapExactTokensForETH");
        }

        [Test]
        public void IsAddLiquidityEth()
        {
            var signatures = AbiHelper.GenerateFunctionSignatures(_abi);
            var input = System.IO.File.ReadAllText(@"TestData\InputAddLiquidityEth.txt");
            var signature = input.Substring(2, 8);
            var result = signatures.First(x => x.Signature == signature);

            Assert.AreEqual(result.FunctionName, "addLiquidityETH");
        }

        [Test]
        public void UniswapV2DecoderInputAddLiquidity()
        {
            var contract = new Contract(new EthApiContractService(null), _abi, "0x7a250d5630b4cf539739df2c5dacb4c659f2488d");
            var input = System.IO.File.ReadAllText(@"TestData\InputAddLiquidityEth.txt");
            var functionSignature = input.Substring(2, 8);
            var function = contract.GetFunctionBySignature(functionSignature);
            var decodedInput = function.DecodeInput(input);

            var result = UniswapV2DecoderHelper.AddLiquidity(decodedInput);
            var expect = "[0.000000000001622391;0.000000000001630544;0.000000001601793664] 0x2260fac5e5542a773aa44fbcfedf7c193bc2c599";

            Assert.AreEqual(result, expect);
        }

        [Test]
        public void UniswapV2DecoderInputInputRemoveLiquidityEthWithPermit()
        {
            var contract = new Contract(new EthApiContractService(null), _abi, "0x7a250d5630b4cf539739df2c5dacb4c659f2488d");
            var input = System.IO.File.ReadAllText(@"TestData\InputRemoveLiquidityEthWithPermit.txt");
            var functionSignature = input.Substring(2, 8);
            var function = contract.GetFunctionBySignature(functionSignature);
            var decodedInput = function.DecodeInput(input);

            var result = UniswapV2DecoderHelper.RemoveLiquidity(decodedInput);
            var expect = "[0.000099310611413773;6.368263960223485228;0.000000002372323555;0.0000000016024238] 0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48";

            Assert.AreEqual(result, expect);
        }

        [Test]
        public void UniswapV2DecoderInputSwapExactEthForTokens()
        {
            var contract = new Contract(new EthApiContractService(null), _abi, "0x7a250d5630b4cf539739df2c5dacb4c659f2488d");
            var input = System.IO.File.ReadAllText(@"TestData\InputSwapExactEthForTokens.txt");
            var functionSignature = input.Substring(2, 8);
            var function = contract.GetFunctionBySignature(functionSignature);
            var decodedInput = function.DecodeInput(input);

            var result = UniswapV2DecoderHelper.SwapExactEthForTokens(decodedInput);
            var expect = "[0.000000000470546365;0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2;0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48;0.000000001601587403] 0x299f770d90334c11f6ae65d770a7ce733f1154d7";

            Assert.AreEqual(result, expect);
        }
        
        [Test]
        public void UniswapV2DecoderInputSwapExactTokensForEth()
        {
            var contract = new Contract(new EthApiContractService(null), _abi, "0x7a250d5630b4cf539739df2c5dacb4c659f2488d");
            var input = System.IO.File.ReadAllText(@"TestData\InputSwapExactTokensForEth.txt");
            var functionSignature = input.Substring(2, 8);
            var function = contract.GetFunctionBySignature(functionSignature);
            var decodedInput = function.DecodeInput(input);

            var result = UniswapV2DecoderHelper.SwapExactTokensForEth(decodedInput);
            var expect = "[49.954040442472772236;0.636555905012287853;0xc011a73ee8576fb46f5e1c5751ca3b9fe0af2a6f;0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2;0.000000001601792828] 0x299f770d90334c11f6ae65d770a7ce733f1154d7";

            Assert.AreEqual(result, expect);
        }

        [Test]
        public void UniswapV2DecoderInputSwapExactTokensForTokens()
        {
            var contract = new Contract(new EthApiContractService(null), _abi, "0x7a250d5630b4cf539739df2c5dacb4c659f2488d");
            var input = System.IO.File.ReadAllText(@"TestData\InputSwapExactTokensForTokens.txt");
            var functionSignature = input.Substring(2, 8);
            var function = contract.GetFunctionBySignature(functionSignature);
            var decodedInput = function.DecodeInput(input);

            var result = UniswapV2DecoderHelper.SwapExactTokensForTokens(decodedInput);
            var expect = "[246.005502229001243195;0.00000000092121904;0x1f9840a85d5af5bf1d1762f925bdaddc4201f984;0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2;0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48;0.000000001601792708] 0x299f770d90334c11f6ae65d770a7ce733f1154d7";

            Assert.AreEqual(result, expect);
        }
    }
}