// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Authenticators;
using GhisTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AssetStore
{
    private readonly IAccountStore accountStore;

    public double AccountBalance => this.accountStore.CurrentAccount?.Balance ?? 0;
    public IEnumerable<AssetTransaction> AssetTransactions => this.accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

    public event Action StateChanged;

    public AssetStore(IAccountStore accountStore)
    {
        this.accountStore = accountStore;

        this.accountStore.AccountChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke();
    }
}