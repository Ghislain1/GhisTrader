// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.ViewModels;

using GhisTrader.Authenticators;
using GhisTrader.Commands;
using GhisTrader.Extensions;
using GhisTrader.Navigators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

public class LoginViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly IRenavigator? loginRenavigator;
    private readonly IRenavigator? renavigator;
    private readonly IRenavigator? registerRenavigator;
    private readonly IAuthenticator? authenticator;
    private string? password;
    private string? userName;
    public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
    {
        this.authenticator = authenticator;
        this.loginRenavigator = loginRenavigator;
        this.registerRenavigator = registerRenavigator;
        this.LoginCommand = new RelayCommand(this.ExecuteLogin, () => this.CanLogin).ObservesProperty(() => this.CanLogin);
        this.ViewRegisterCommand = new RelayCommand(this.ExecuteViewRegister).ObservesProperty(() => this.CanLogin);
    }


    public ICommand LoginCommand { get; }
    public ICommand ViewRegisterCommand { get; }

    public string? UserName
    {
        get => this.userName;
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.userName, value);
    }
    public string? Password
    {
        get => this.password;
        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.password, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanLogin));
            }
        }
    }
    public bool CanLogin => !string.IsNullOrWhiteSpace(this.UserName) && !string.IsNullOrWhiteSpace(this.Password);

    private void ExecuteLogin()
    {

    }

    private void ExecuteViewRegister()
    {
        this.renavigator?.Renavigate();
    }
}