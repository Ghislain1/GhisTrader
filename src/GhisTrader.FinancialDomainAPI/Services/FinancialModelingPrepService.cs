// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.FinancialModelingPrepAPI.Services;

using GhisTrader.Domain;
using GhisTrader.Domain.Exceptions;
using GhisTrader.Domain.Models;
using GhisTrader.FinancialModelingPrepAPI.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FinancialModelingPrepService : IFinancialModelingPrepService
{
    private readonly FinancialModelingPrepHttpClient financialModelingPrepHttpClient;
    private readonly IDataService<Account> accountService;
    public FinancialModelingPrepService(IDataService<Account> accountService,FinancialModelingPrepHttpClient financialModelingPrepHttpClient)
    {
        this.accountService = accountService;
        this.financialModelingPrepHttpClient = financialModelingPrepHttpClient;
    }
    public async Task<Account> SellStock(Account seller, string symbol, int shares)
    {
        // Validate seller has sufficient shares.
        int accountShares = GetAccountSharesForSymbol(seller, symbol);
        if (accountShares < shares)
        {
            throw new InsufficientSharesException(symbol, accountShares, shares);
        }

        double stockPrice = await this.GetPrice(symbol);

        seller.AssetTransactions.Add(new AssetTransaction()
        {
            Account = seller,
            Asset = new Asset()
            {
                PricePerShare = stockPrice,
                Symbol = symbol
            },
            DateProcessed = DateTime.Now,
            IsPurchase = false,
            Shares = shares
        });

        seller.Balance += stockPrice * shares;

        await  this.accountService.Update(seller.Id, seller);

        return seller;
    }

    private int GetAccountSharesForSymbol(Account seller, string symbol)
    {
        IEnumerable<AssetTransaction> accountTransactionsForSymbol = seller.AssetTransactions.Where(a => a.Asset.Symbol == symbol);

        return accountTransactionsForSymbol.Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
    }
    public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
    {
        double stockPrice = await this.GetPrice(symbol);

        double transactionPrice = stockPrice * shares;

        if (transactionPrice > buyer.Balance)
        {
            throw new InsufficientFundsException(buyer.Balance, transactionPrice);
        }

        AssetTransaction transaction = new AssetTransaction()
        {
            Account = buyer,
            Asset = new Asset()
            {
                PricePerShare = stockPrice,
                Symbol = symbol
            },
            DateProcessed = DateTime.Now,
            Shares = shares,
            IsPurchase = true
        };

        buyer.AssetTransactions.Add(transaction);
        buyer.Balance -= transactionPrice;

        await  this.accountService.Update(buyer.Id, buyer);

        return buyer;
    }
    public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
    {
        string uri = "majors-indexes/" + GetUriSuffix(indexType);

        MajorIndex majorIndex = await this.financialModelingPrepHttpClient.GetAsync<MajorIndex>(uri);
        majorIndex.Type = indexType;

        return majorIndex;
    }
    public async Task<double> GetPrice(string symbol)
    {
        string uri = "stock/real-time-price/" + symbol;

        var stockPriceResult = await financialModelingPrepHttpClient.GetAsync<StockPriceResult>(uri);

        if (stockPriceResult.Price == 0)
        {
            throw new InvalidSymbolException(symbol);
        }

        return stockPriceResult.Price;
    }
    private string GetUriSuffix(MajorIndexType indexType)
    {
        switch (indexType)
        {
            case MajorIndexType.DowJones:
                return ".DJI";
            case MajorIndexType.Nasdaq:
                return ".IXIC";
            case MajorIndexType.SP500:
                return ".INX";
            default:
                throw new Exception("MajorIndexType does not have a suffix defined.");
        }
    }
}
