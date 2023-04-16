// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.EntityFramework.Services
{
    using GhisTrader.Domain;
    using GhisTrader.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    // GZe: Why Implem hier in this Project ?
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialDomainHttpClient _client;

        public StockPriceService(FinancialDomainHttpClient financialDomainHttpClient)
        {
            _client = financialDomainHttpClient;
        }

        public async Task<double> GetPrice(string symbol)
        {
            string uri = "stock/real-time-price/" + symbol;

            StockPriceResult stockPriceResult = await _client.GetAsync<StockPriceResult>(uri);

            if (stockPriceResult.Price == 0)
            {
                throw new InvalidSymbolException(symbol);
            }

            return stockPriceResult.Price;
        }
    }
}
