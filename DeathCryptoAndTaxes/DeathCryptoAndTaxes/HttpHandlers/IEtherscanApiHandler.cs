using DeathCryptoAndTaxes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeathCryptoAndTaxes.HttpHandlers
{
    public interface IEtherscanApiHandler
    {
        Task<IEnumerable<EthereumTx>> GetTransactionsAsync(string address);
        Task<string> GetContractAbiAsync(string address);
    }
}
