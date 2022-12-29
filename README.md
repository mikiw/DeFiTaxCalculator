
# Death Crypto And Taxes
Death Crypto And Taxes, only sure things to come in the future! Still not sure about death...

## Info
I started this project by getting transactions from Etherscan. Later I realized that Etherscan doesn't provide all data in a clean way as they do on-page so to interpret contracts transactions inputs so I used the Nethereum library. 

I'm not sure if any API provides clean data from Uniswap contracts, maybe tenderly.co does. Anyway, I decided to go low level with reading contract inputs (https://docs.soliditylang.org/en/develop/abi-spec.html). This solution reads all contracts inputs so it can also handle other DeFi protocols even when APIs for them don't exist.

If I could back time I would use node.js and web3, it would be easier and faster to write this in JS comparing to exotic Nethereum in C#. Nethereum is poorly documented and a lot of basic web3 functions are still missing there.

For Http calls I used IHttpClientFactory client to avoid socket exhaustion (https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/).

It's a console app but this solution can be adopted as scalable Azure function, AWS lambda, or your KT8s infrastructure.

Results of processing are in file with really authentically and pioneer name "Results.txt".
##Example

```

TxId: 0x2ca4428aa133a21299d8aece0096c24bc9375792054d4ea886b827752c287f37
TimeStamp: 1601792511
From: 0x299f770d90334c11f6ae65d770a7ce733f1154d7
To: 0x7a250d5630b4cf539739df2c5dacb4c659f2488d
Amount: 0.49834840440212511
CumulativeGasUsed: 6782887
Type: Contract interaction
Protocol: Uniswap (v2)
ContractFunction: addLiquidityETH [0.000000000001622391;0.000000000001630544;0.000000001601793664] 0x2260fac5e5542a773aa44fbcfedf7c193bc2c599

TxId: 0xb089ba25f6dc04ed45215abd6673690de8023766868bbc038fc8a1bb9d01bc5f
TimeStamp: 1601792193
From: 0x299f770d90334c11f6ae65d770a7ce733f1154d7
To: 0x7a250d5630b4cf539739df2c5dacb4c659f2488d
Amount: 0.5
CumulativeGasUsed: 11925192
Type: Contract interaction
Protocol: Uniswap (v2)
ContractFunction: swapExactETHForTokens [0.000000000001622375;0xc02aaa39b223fe8d0a0e5c4f27ead9083c756cc2;0x2260fac5e5542a773aa44fbcfedf7c193bc2c599;0.000000001601793317] 0x299f770d90334c11f6ae65d770a7ce733f1154d7
```

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
- Handle 0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48 contract, it was impossible to read signature (EVM optimalization)
- Implement FlowFactory to handle other blockchain networks and avoid if else if else if
- Implement other contracts on this wallet
- Implement reading for Wrapped BTC: WBTC Token contract 0x2260fac5e5542a773aa44fbcfedf7c193bc2c599
- Implement walletsToProcess as async tasks
- Implement contract definitions as classes for Nethereum for static typing (I tried to do that but auto-generated code from plugin was a mess)
