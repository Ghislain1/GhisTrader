﻿// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Extensions;

using GhisTrader.Authenticators;
using GhisTrader.Navigators;
using GhisTrader.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AddViewModelsHostBuilderExtension
{
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            Console.WriteLine("------------ Main Registr");
            services.AddTransient<MainViewModel>();

            // Add VM i.e.LoginViewModel
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
            services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

        });
        return hostBuilder;
    }
    private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
    {
        return new LoginViewModel(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
    }

    private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
    {
        return new RegisterViewModel(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
    }
}