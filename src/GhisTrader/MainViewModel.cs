// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader;

using GhisTrader.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private bool isLoggedIn;
    public MainViewModel()
    {
        this.LoadAsync();
    }

    private async void LoadAsync()
    {
        int s = 0;
        while(s < 100)
        {
            await Task.Delay(1000);
            s++;
            this.IsLoggedIn = !IsLoggedIn;
        }
    }

    public  bool IsLoggedIn
    {
        get => this.isLoggedIn;
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.isLoggedIn , value);
    }
}

