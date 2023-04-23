// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Extensions;

using GhisTrader.Authenticators;
using GhisTrader.Domain;
using GhisTrader.Domain.Models;
using GhisTrader.EntityFramework.Factory;
using GhisTrader.EntityFramework.Services;
using GhisTrader.FinancialModelingPrepAPI.Services;
using GhisTrader.Navigators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AddSingletonServicesHostBuilderExtension
{
    public static IHostBuilder AddSingletonServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<AssetStore>();
            services.AddSingleton<ITraderDbContextFactory, TraderDbContextFactory>();

            // TODO: Understand
            services.AddSingleton<IAccountStore, AccountStore>();

            services.AddSingleton<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
            // Why 2 because IAuthenticationService exists by Microsoft
            services.AddSingleton<IAuthenticationService2, AuthenticationService2>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            //services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IFinancialModelingPrepService, FinancialModelingPrepService>();
            //services.AddSingleton<IBuyStockService, BuyStockService>();
            //services.AddSingleton<ISellStockService, SellStockService>();
           // services.AddSingleton<IMajorIndexService, MajorIndexService>();



        });
        return hostBuilder;
    }
}