using DeathCryptoAndTaxes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeathCryptoAndTaxes.Flows
{
    interface IEthereumTaxFlow
    {
        Task<IEnumerable<EthereumTxTaxEvent>> GetTaxEventsFromWalletAsync(string wallet);
    }
}
