using Newtonsoft.Json;

namespace DeathCryptoAndTaxes.Models
{
    public class EthereumTx
    {
        public string BlockNumber { get; set; }
        public string TimeStamp { get; set; }
        public string Hash { get; set; }
        public string Nonce { get; set; }
        public string BlockHash { get; set; }
        public string TransactionIndex { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Value { get; set; }
        public string Gas { get; set; }
        public string GasPrice { get; set; }
        public string IsError { get; set; }
        public string Input { get; set; }
        public string ContractAddress { get; set; }
        public string CumulativeGasUsed { get; set; }
        public string GasUsed { get; set; }
        public string Confirmations { get; set; }

        [JsonProperty(PropertyName = "txreceipt_status")]
        public string TxReceiptStatus { get; set; }
    }
}
