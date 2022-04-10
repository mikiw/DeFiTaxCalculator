namespace DeathCryptoAndTaxes.Models
{
    public class EthereumTxTaxEvent
    {
        public string TxId { get; set; }
        public string TimeStamp { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Type { get; set; }
        public string Protocol { get; set; }
        public decimal Amount { get; set; }
        public string ContractFunction { get; set; }
        public string GasUsed { get; set; }
    }
}
