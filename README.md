
# Death Crypto And Taxes
Death Crypto And Taxes, only sure things to come in the future! Still not sure about death...

## Info
I started my task by getting transactions from Etherscan. Later I realized that Etherscan doesn't provide all data in a clean way as they do on-page so to interpret contracts transactions inputs I used the Nethereum library. 

I'm not sure if any API provides clean data from Uniswap contracts, maybe tenderly.co does. Anyway, I wanted to "read in all ETH transfers" and avoid too many APIs requests so decided to go low level with reading contract inputs (https://docs.soliditylang.org/en/develop/abi-spec.html). This solution reads all contracts inputs so it can also handle other DeFi protocols even when APIs for them don't exist.

This solution can also be rewritten to use only web3 so we could only interact with a local or remote Ethereum node using HTTP, IPC or WebSocket and avoid Etherscan or any APIs. We could also get prices of tokens from oracles. I guess it's a topic for another conversation whether to use API or not, it's a question about money, security (DNS spoofing), scalability, SLA etc..

If I could back time I would use node.js and web3, it would be easier and faster to write this in JS (which I hate) comparing to exotic Nethereum in C#. Nethereum is poorly documented and a lot of basic web3 functions are still missing there.

For Http calls I used IHttpClientFactory client to avoid socket exhaustion (https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/).

It's a console app but this solution can be adopted as scalable Azure function, AWS lambda, or your KT8s infrastructure.

Results of processing are in file with really authentically and pioneer name "Results.txt".

## Things TODO later
If I had infinite time resources I would do these things:
- Currently, API calls in EtherscanHandler can only handle 10k records, paging is needed to be implemented to read all transactions (even when the number is astronomical)
- Add integration tests for EthereumTaxFlow and EtherscanApiHandler, also add more unit tests and test program on other wallets
- Add ILogger for logs
- Get tickers from contracts addresses to be more readable
- Get Eth price in dollars, UniswapV2 GraphQLRequest ({ bundle(id: '1', block: { number: " + blockHeight + "}) { ethPrice }})
- Get token prices in dollars
- Implement tax calculation
- Contracts ABIs can be cached or even stored in the system to avoid unnecessary calls (it's tricky when contract has upgrading mechanism -_-')
- Handle 0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48 contract, it was impossible to read signature (don't know why)
- Implement FlowFactory to handle other blockchain networks and avoid if else if else if
- Implement other contracts on this wallet
- Implement reading for Wrapped BTC: WBTC Token contract 0x2260fac5e5542a773aa44fbcfedf7c193bc2c599
- Implement walletsToProcess as async tasks
- Implement contract definitions as classes for Nethereum for static typing (I tried to do that but auto-generated code from plugin was a mess)
