using System.Collections.Generic;

namespace DeathCryptoAndTaxes.Models
{
    public class EtherscanTxResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<EthereumTx> Result { get; set; }
    }
}
