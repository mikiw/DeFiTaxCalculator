using Nethereum.ABI.FunctionEncoding;
using Nethereum.Web3;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DeathCryptoAndTaxes.Helpers
{
    /// <summary>
    /// Helper class to decode Uniswap v2 parameters.
    /// </summary>
    public static class UniswapV2DecoderHelper
    {
        private static decimal ParameterOutputBigInteger(List<ParameterOutput> decodedInput, string name)
        {
            return Web3.Convert.FromWei((BigInteger)decodedInput.First(x => x.Parameter.Name == name).Result);
        }

        public static string AddLiquidity(List<ParameterOutput> decodedInput)
        {
            var token = decodedInput.First(x => x.Parameter.Name == "token").Result;
            var amountTokenMin = ParameterOutputBigInteger(decodedInput, "amountTokenMin");
            var amountTokenDesired = ParameterOutputBigInteger(decodedInput, "amountTokenDesired");
            var deadline = ParameterOutputBigInteger(decodedInput, "deadline");

            return $"[{amountTokenMin};{amountTokenDesired};{deadline}] {token}";
        }

        public static string RemoveLiquidity(List<ParameterOutput> decodedInput)
        {
            var token = decodedInput.First(x => x.Parameter.Name == "token").Result;
            var liquidity = ParameterOutputBigInteger(decodedInput, "liquidity");
            var amountETHMin = ParameterOutputBigInteger(decodedInput, "amountETHMin");
            var amountTokenMin = ParameterOutputBigInteger(decodedInput, "amountTokenMin");
            var deadline = ParameterOutputBigInteger(decodedInput, "deadline");

            return $"[{liquidity};{amountETHMin};{amountTokenMin};{deadline}] {token}";
        }

        public static string SwapExactEthForTokens(List<ParameterOutput> decodedInput)
        {
            var amountOutMin = ParameterOutputBigInteger(decodedInput, "amountOutMin");
            var to = decodedInput.First(x => x.Parameter.Name == "to").Result;
            var deadline = ParameterOutputBigInteger(decodedInput, "deadline");
            var path = (List<object>)decodedInput.First(x => x.Parameter.Name == "path").Result;

            return $"[{amountOutMin};{path[0]};{path[1]};{deadline}] {to}";
        }

        public static string SwapExactTokensForEth(List<ParameterOutput> decodedInput)
        {
            var amountIn = ParameterOutputBigInteger(decodedInput, "amountIn");
            var amountOutMin = ParameterOutputBigInteger(decodedInput, "amountOutMin");
            var to = decodedInput.First(x => x.Parameter.Name == "to").Result;
            var deadline = ParameterOutputBigInteger(decodedInput, "deadline");
            var path = (List<object>)decodedInput.First(x => x.Parameter.Name == "path").Result;

            return $"[{amountIn};{amountOutMin};{path[0]};{path[1]};{deadline}] {to}";
        }

        public static string SwapExactTokensForTokens(List<ParameterOutput> decodedInput)
        {
            var amountIn = ParameterOutputBigInteger(decodedInput, "amountIn");
            var amountOutMin = ParameterOutputBigInteger(decodedInput, "amountOutMin");
            var to = decodedInput.First(x => x.Parameter.Name == "to").Result;
            var deadline = ParameterOutputBigInteger(decodedInput, "deadline");
            var path = (List<object>)decodedInput.First(x => x.Parameter.Name == "path").Result;

            return $"[{amountIn};{amountOutMin};{path[0]};{path[1]};{path[2]};{deadline}] {to}";
        }
    }
}
