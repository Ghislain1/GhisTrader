// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Extensions;

using GhisTrader.EntityFramework;
using GhisTrader.FinancialModelingPrepAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class AddFinanceAPIHostBuilderExtensions
{
    public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            // "https://financialmodelingprep.com/api/v3/income-statement/AAPL?apikey=0afb2ef442b712afa5c85b022d5a1f6e"))
            var defaultApiKey = "0afb2ef442b712afa5c85b022d5a1f6e";
            string apiKey = context.Configuration.GetValue<string>("FINANCE_API_KEY")?? defaultApiKey;

            services.AddSingleton(new FinancialModelingPrepAPIKey(apiKey));

            services.AddHttpClient<FinancialModelingPrepHttpClient>(c =>
            {
                c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
            });
        });

        return host;
    }
}