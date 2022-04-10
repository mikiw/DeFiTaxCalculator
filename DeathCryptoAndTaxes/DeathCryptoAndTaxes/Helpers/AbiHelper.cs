using DeathCryptoAndTaxes.Models;
using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DeathCryptoAndTaxes.Helpers
{
    public static class AbiHelper
    {
        /// <summary>
        /// Generate functions signatures from abi JSON.
        /// </summary>
        /// <param name="abi">Abi contract as JSON</param>
        /// <returns>IEnumerable of signatures and names</returns>
        public static IEnumerable<AbiSignature> GenerateFunctionSignatures(string abi)
        {
            var abiRecords = JsonConvert.DeserializeObject<List<AbiRecord>>(abi);
            var abiFunctions = abiRecords.Where(x => x.Type == "function");
            var abiEncode = new ABIEncode();
            var functionsSignatures = new List<AbiSignature>();

            foreach (var abiFunction in abiFunctions)
            {
                var functionInputs = abiFunction.Inputs.Count() > 0 ? 
                    abiFunction.Inputs.Select(x => x.Type).Aggregate((s1, s2) => s1 + "," + s2) : "";
                var functionHash = abiEncode.GetSha3ABIEncodedPacked($"{abiFunction.Name}({functionInputs})");

                functionsSignatures.Add(
                    new AbiSignature()
                    { 
                        FunctionName = abiFunction.Name,
                        Signature = functionHash.ToHex().Substring(0, 8) // take 4 bytes for signature
                    }
                );
            }

            return functionsSignatures;
        }
    }
}
