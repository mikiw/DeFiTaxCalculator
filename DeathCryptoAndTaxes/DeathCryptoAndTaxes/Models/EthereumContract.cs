using Nethereum.Contracts;
using System.Collections.Generic;

namespace DeathCryptoAndTaxes.Models
{
    public class EthereumContract
    {
        public Contract Contract { get; set; }
        public IEnumerable<AbiSignature> Signatures { get; set; }
        public Protocol? Protocol { get; set; }
    }
}
