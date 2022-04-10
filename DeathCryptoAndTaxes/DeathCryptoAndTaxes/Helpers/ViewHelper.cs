using DeathCryptoAndTaxes.Models;
using System;
using System.Collections.Generic;

namespace DeathCryptoAndTaxes.Helpers
{
    public static class ViewHelper
    {
        public static void Display(IEnumerable<EthereumTxTaxEvent> taxEvents)
        {
            foreach (var item in taxEvents)
            {
                Console.WriteLine($"TxId: {item.TxId}");
                Console.WriteLine($"TimeStamp: {item.TimeStamp}");
                Console.WriteLine($"From: {item.From}");
                Console.WriteLine($"To: {item.To}");
                Console.WriteLine($"Amount: {item.Amount}");
                Console.WriteLine($"CumulativeGasUsed: {item.GasUsed}");
                Console.WriteLine($"Type: {item.Type}");
                Console.WriteLine($"Protocol: {item.Protocol}");
                Console.WriteLine($"ContractFunction: {item.ContractFunction}");
                Console.WriteLine();
            }
        }
    }
}
