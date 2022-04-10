using System;
using System.Collections.Generic;
using System.Text;

namespace DeathCryptoAndTaxes.Helpers
{

    public class UniswapV2MainnetHelper
    {
        /// <summary>
        /// Get UniswapV2 trusted addresses from ethereum blockchain. Can be cached or stored in the system.
        /// https://uniswap.org/docs/v2/smart-contracts/router02/
        /// https://docs.uniswap.org/protocol/concepts/governance/overview
        /// 
        /// This could be also read from blockchain itself from main contract creator and events
        /// but nothing is good as old fashioned strings in static class.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAddresses()
        {
            var addresses = new List<string>();

            // Uniswap V2: Router 2
            addresses.Add("0x7a250d5630b4cf539739df2c5dacb4c659f2488d");

            // The UNI merkle distributor address
            addresses.Add("0x090D4613473dEE047c3f2706764f49E0821D256e");

            // The four staking rewards addresses
            addresses.Add("0x6c3e4cb2e96b01f4b866965a91ed4437839a121a");
            addresses.Add("0x7fba4b8dc5e7616e59622806932dbea72537a56b");
            addresses.Add("0xa1484c3aa22a66c62b77e0ae78e15258bd0cb711");
            addresses.Add("0xca35e32e7926b96a9988f61d510e038108d8068e");

            // The four year - long vesting contract addresses are
            addresses.Add("0x4750c43867ef5f89869132eccf19b9b6c4286e1a");
            addresses.Add("0xe3953d9d317b834592ab58ab2c7a6ad22b54075d");
            addresses.Add("0x4b4e140d1f131fdad6fb59c13af796fd194e4135");
            addresses.Add("0x3d30b1ab88d487b0f3061f40de76845bec3f1e94");

            return addresses;
        }
    }
}
