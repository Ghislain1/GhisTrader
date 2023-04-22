// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AddViewsHostBuilderExtension
{
    public static IHostBuilder AddViews(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            Console.WriteLine("------------ Main Registr");
            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

        });
        return hostBuilder;
    }
}