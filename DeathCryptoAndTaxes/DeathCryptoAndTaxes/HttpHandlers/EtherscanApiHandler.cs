using DeathCryptoAndTaxes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeathCryptoAndTaxes.HttpHandlers
{
    public class EtherscanApiHandler : IEtherscanApiHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Configs _configs;

        public EtherscanApiHandler(IHttpClientFactory httpClientFactory, Configs configs)
        {
            _httpClientFactory = httpClientFactory;
            _configs = configs;
        }

        public async Task<IEnumerable<EthereumTx>> GetTransactionsAsync(string address)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var etherscanApiUrl = string.Format(_configs.EtherscanTxListUrl, address, _configs.EtherscanApiKey);
                var response = await client.GetAsync(new Uri(etherscanApiUrl).AbsoluteUri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var payload = await response.Content.ReadAsStringAsync();
                    var etherscanResponse = JsonConvert.DeserializeObject<EtherscanTxResponse>(payload);

                    return etherscanResponse.Result;
                }
                else
                {
                    throw new Exception($"Failed to fetch data - StatusCode: {response.StatusCode} - {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetContractAbiAsync(string address)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var etherscanApiUrl = string.Format(_configs.EtherscanGetAbitUrl, address, _configs.EtherscanApiKey);
                var response = await client.GetAsync(new Uri(etherscanApiUrl).AbsoluteUri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var payload = await response.Content.ReadAsStringAsync();
                    var etherscanResponse = JsonConvert.DeserializeObject<EtherscanAbiResponse>(payload);

                    return etherscanResponse.Result;
                }
                else
                {
                    throw new Exception($"Failed to fetch data - StatusCode: {response.StatusCode} - {response.Content}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
