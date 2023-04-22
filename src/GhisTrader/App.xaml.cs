// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader;

using GhisTrader.EntityFramework.DbContexts;
using GhisTrader.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost? host;
    private readonly AppUserDbContext appUserDbContext = new AppUserDbContext();
    public App()
    {
        this.host = CreateHostBuider().Build();
    }

    public static IHostBuilder CreateHostBuider(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
             .AddSingletonServices()
               .AddViewModels()
               .AddViews();
    }
    protected override async void OnStartup(StartupEventArgs startupEventArgs)
    {
        await this.host?.StartAsync()!;

        // DB Migration
        await this.appUserDbContext.Database.EnsureCreatedAsync();

        // load the entities into EF Core
        this.appUserDbContext.AppUsers.Load();


        // Show Window
        var shell = this.host?.Services.GetRequiredService<MainWindow>();
        shell?.Show();
        base.OnStartup(startupEventArgs);
    }
    protected override async void OnExit(ExitEventArgs exitEventArgs)
    {
        await this.host?.StopAsync()!;

        this.host?.Dispose();
        base.OnExit(exitEventArgs);
    }
}
