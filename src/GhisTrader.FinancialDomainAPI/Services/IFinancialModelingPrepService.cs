﻿// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.FinancialModelingPrepAPI.Services;

using GhisTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Retrieve MajorIndex, Stock Price and more from API...
/// </summary>
public interface IFinancialModelingPrepService
{
    Task<MajorIndex> GetMajorIndex(MajorIndexType indexType);
    /// <summary>
    /// Sell a stock for an account.
    /// </summary>
    /// <param name="seller">The account of the seller.</param>
    /// <param name="symbol">The symbol sold.</param>
    /// <param name="shares">The amount of shares to sell.</param>
    /// <returns>The updated account.</returns>
    /// <exception cref="InsufficientSharesException">Thrown if the seller has insufficient shares for the symbol.</exception>
    /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
    /// <exception cref="Exception">Thrown if the transaction fails.</exception>
    Task<Account> SellStock(Account seller, string symbol, int shares);

    /// <summary>
    /// Purchase a stock for an account.
    /// </summary>
    /// <param name="buyer">The account of the buyer.</param>
    /// <param name="symbol">The symbol purchased.</param>
    /// <param name="shares">The amount of shares.</param>
    /// <returns>The updated account.</returns>
    /// <exception cref="InsufficientFundsException">Thrown if the acccount has an insufficient balance.</exception>
    /// <exception cref="InvalidSymbolException">Thrown if the purchased symbol is invalid.</exception>
    /// <exception cref="Exception">Thrown if the transaction fails.</exception>
    Task<Account> BuyStock(Account buyer, string symbol, int shares);

    /// <summary>
    /// Get the share price for a symbol.
    /// </summary>
    /// <param name="symbol">The symbol to get the price of.</param>
    /// <returns>The price of symbol.</returns>
    /// <exception cref="InvalidSymbolException">Thrown if symbol does not exist.</exception>
    /// <exception cref="Exception">Thrown if getting the symbol fails.</exception>
    Task<double> GetPrice(string symbol);
}
