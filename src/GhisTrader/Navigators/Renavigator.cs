// <copyright company="Ghislain One Inc.">
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

public class Renavigator<TViewModel> : IRenavigator where TViewModel : INotifyPropertyChanged
{
    private readonly INavigator navigator;
    // TODO@Ze can replace with action ?
    private readonly CreateViewModel<TViewModel> createViewModel;
    public Renavigator(INavigator navigator,CreateViewModel<TViewModel> createViewModel)
    {
        this.navigator= navigator;
        this.createViewModel= createViewModel;
    }
    public void Renavigate()
    {
        this.navigator.CurrentViewModel = this.createViewModel();
    }
}
