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

public class AccountStore : IAccountStore
{
    private Account? currentAccount;
    public Account? CurrentAccount
    {
        get
        {
            return this.currentAccount;
        }
        set
        {
            this.currentAccount = value;
            this.AccountChanged?.Invoke();
        }
    }

    public event Action? AccountChanged;

}