using System.Collections.Generic;

namespace DeathCryptoAndTaxes.Models
{
    public class AbiSignature
    {
        public string Signature { get; set; }
        public string FunctionName { get; set; }
    }

    public class AbiRecord
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<AbiInput> Inputs { get; set; }
    }

    public class AbiInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Indexed { get; set; }
    }
}
