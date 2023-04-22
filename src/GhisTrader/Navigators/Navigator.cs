﻿// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Navigators;

using GhisTrader.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Navigator : INavigator
{

    private INotifyPropertyChanged? currentViewModel;

    public INotifyPropertyChanged? CurrentViewModel
    {
        get
        {
            return this.currentViewModel;
        }
        set
        {

            if (this.currentViewModel != value)
            {
                this.currentViewModel = value;
                this.StateChanged?.Invoke();
            }
        }
    }

    public event Action? StateChanged;

}