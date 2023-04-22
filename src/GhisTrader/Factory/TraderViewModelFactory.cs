// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Factory;
using GhisTrader.Extensions;
using GhisTrader.Navigators;
using GhisTrader.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TraderViewModelFactory : ITraderViewModelFactory
{
    private readonly CreateViewModel<HomeViewModel> createHomeViewModel;
    private readonly CreateViewModel<PortfolioViewModel> createPortfolioViewModel;
    private readonly CreateViewModel<LoginViewModel> createLoginViewModel;
    private readonly CreateViewModel<BuyViewModel> createBuyViewModel;
    private readonly CreateViewModel<SellViewModel> createSellViewModel;

    public TraderViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
        CreateViewModel<PortfolioViewModel> createPortfolioViewModel,
        CreateViewModel<LoginViewModel> createLoginViewModel,
        CreateViewModel<BuyViewModel> createBuyViewModel,
        CreateViewModel<SellViewModel> createSellViewModel)
    {
        this.createHomeViewModel = createHomeViewModel;
        this.createPortfolioViewModel = createPortfolioViewModel;
        this.createLoginViewModel = createLoginViewModel;
        this.createBuyViewModel = createBuyViewModel;
        this.createSellViewModel = createSellViewModel;
    }

    public INotifyPropertyChanged CreateViewModel(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.Login:
                return this.createLoginViewModel();
            case ViewType.Home:
                return this.createHomeViewModel();
            case ViewType.Portofolio:
                return this.createPortfolioViewModel();
            case ViewType.Buy:
                return this.createBuyViewModel();
            case ViewType.Sell:
                return this.createSellViewModel();
            default:
                throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
        }
    }
}