// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.ViewModels;

using GhisTrader.Domain.Models;
using GhisTrader.Domain;
using System.Windows.Input;
using System.ComponentModel;
using GhisTrader.Extensions;
using GhisTrader.Commands;
using System;
using GhisTrader.EntityFramework.Services;
using System.Threading.Tasks;
using GhisTrader.FinancialModelingPrepAPI.Services;

public class MajorIndexListingViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly IFinancialModelingPrepService? financialModelingPrepService;
    private MajorIndex dowJones;
    public MajorIndex DowJones
    {
        get
        {
            return this.dowJones;
        }
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.dowJones, value);

    }

    private MajorIndex nasdaq;
    public MajorIndex Nasdaq
    {
        get
        {
            return this.nasdaq;
        }
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.nasdaq, value);
    }

    private MajorIndex sp500;
    public MajorIndex SP500
    {
        get
        {
            return this.sp500;
        }
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.sp500, value);
    }

    private bool isLoading;
    public bool IsLoading
    {
        get
        {
            return this.isLoading;
        }
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.isLoading, value);
    }

    public ICommand LoadMajorIndexesCommand { get; }

    public MajorIndexListingViewModel(IFinancialModelingPrepService financialModelingPrepService)
    {
        this.financialModelingPrepService = financialModelingPrepService;
        this.LoadMajorIndexesCommand = new RelayCommand(this.ExecuteLoadMajorIndexes, () => true);
    }

    private async void ExecuteLoadMajorIndexes()
    {
        this.IsLoading = true;

        await Task.WhenAll(LoadDowJones(), LoadNasdaq(), LoadSP500());

        this.IsLoading = false;
    }

    public static MajorIndexListingViewModel LoadMajorIndexViewModel(IFinancialModelingPrepService financialModelingPrepService)
    {
        MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(financialModelingPrepService);

        majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

        return majorIndexViewModel;
    }



    private async Task LoadDowJones()
    {
        this.DowJones = await this.financialModelingPrepService!.GetMajorIndex(MajorIndexType.DowJones);
    }

    private async Task LoadNasdaq()
    {
        this.Nasdaq = await this.financialModelingPrepService!.GetMajorIndex(MajorIndexType.Nasdaq);
    }

    private async Task LoadSP500()
    {
        this.SP500 = await this.financialModelingPrepService!.GetMajorIndex(MajorIndexType.SP500);
    }
}