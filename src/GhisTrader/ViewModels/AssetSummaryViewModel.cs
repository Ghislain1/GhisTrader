// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.ViewModels
{
    using GhisTrader.Authenticators;
    using GhisTrader.Extensions;
    using System;
    using System.ComponentModel;

    public class AssetSummaryViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly AssetStore assetStore;

        public double AccountBalance => this.assetStore.AccountBalance;
        public AssetListingViewModel AssetListingViewModel { get; }

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            this.assetStore = assetStore;
            AssetListingViewModel = new AssetListingViewModel(assetStore, assets => assets.Take(3));

            this.assetStore.StateChanged += this.AssetStore_StateChanged;
        }

        private void AssetStore_StateChanged()
        {
            
            this.InvokePropertyChanged(this.PropertyChanged, nameof(this.AccountBalance));
        }

        public   void Dispose()
        {
            //this.assetStore.StateChanged -= AssetStorethis.StateChanged;
            //AssetListingViewModel.Dispose();

            //base.Dispose();
        }
    }
}