// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.EntityFramework
{
    public class FinancialDoaminAPIKey
    {
        public string Key { get; }

        public FinancialDoaminAPIKey(string key)
        {
            Key = key;
        }
    }
}