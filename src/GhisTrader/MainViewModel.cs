// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader;

using GhisTrader.Authenticators;
using GhisTrader.Extensions;
using GhisTrader.Factory;
using GhisTrader.Navigators;
using GhisTrader.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private INotifyPropertyChanged? currentViewModel;
    private bool isLoggedIn;
    private readonly INavigator navigator;
    private readonly IAuthenticator authenticator;
    private readonly ITraderViewModelFactory traderViewModelFactory;
    public MainViewModel(ITraderViewModelFactory traderViewModelFactory, INavigator navigator, IAuthenticator authenticator)
    {
        this.traderViewModelFactory = traderViewModelFactory;
        this.navigator = navigator;
        this.authenticator = authenticator;
        this.navigator.StateChanged += () => {

        
        };
        this.authenticator.StateChanged += () => { };
        this.CurrentViewModel = this.traderViewModelFactory.CreateViewModel(ViewType.Login);

    }

    private async void LoadAsync()
    {
        int s = 0;
        while (s < 100)
        {
            await Task.Delay(1000);
            s++;
            this.IsLoggedIn = !IsLoggedIn;
        }
    }

    public bool IsLoggedIn
    {
        get => this.isLoggedIn;
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.isLoggedIn, value);
    }
    public INotifyPropertyChanged? CurrentViewModel
    {
        get => this.currentViewModel;
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.currentViewModel, value);
    }


}