// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.ViewModels;

using GhisTrader.Authenticators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

public class AssetListingViewModel: INotifyPropertyChanged
{
    private readonly AssetStore  assetStore;
    private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>>  filterAssets;
    private readonly ObservableCollection<AssetViewModel> assets;
    public event PropertyChangedEventHandler? PropertyChanged;
    public IEnumerable<AssetViewModel> Assets => this.assets;
 
    public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assets => assets) { }

    public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
    {
        this.assetStore = assetStore;
        this.filterAssets = filterAssets;
        this.assets = new ObservableCollection<AssetViewModel>();

        this.assetStore.StateChanged += AssetStore_StateChanged;

       this. ResetAssets();
    }

    private void ResetAssets()
    {
        IEnumerable<AssetViewModel> assetViewModels = this.assetStore.AssetTransactions
            .GroupBy(t => t.Asset.Symbol)
            .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
            .Where(a => a.Shares > 0)
            .OrderByDescending(a => a.Shares);

        assetViewModels = this.filterAssets(assetViewModels);

        DisposeAssets();
        this.assets.Clear();
        foreach (AssetViewModel viewModel in assetViewModels)
        {
            this.assets.Add(viewModel);
        }
    }

    private void DisposeAssets()
    {
        foreach (AssetViewModel asset in this.assets)
        {
           // asset.Dispose();
        }
    }

    private void AssetStore_StateChanged()
    {
        ResetAssets();
    }

}