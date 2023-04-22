// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.ViewModels;

using GhisTrader.Commands;
using GhisTrader.Extensions;
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

    private  string ?password;
    private string? userName;
    public LoginViewModel()
    {
        this.LoginCommand = new RelayCommand(this.ExecuteLogin, () => this.CanLogin).ObservesProperty(() => this.CanLogin);
    }

    private void ExecuteLogin( )
    {
         
    }

    public string? UserName
    {
        get => this.userName;
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.userName, value);
    }
    public RelayCommand LoginCommand { get; }
    public string? Password
    {
        get => this.password;
        set
        {
            if(this.InvokePropertyChanged(this.PropertyChanged, ref this.password, value))
            {
                
            }
        }
    }
    public bool CanLogin => !string.IsNullOrWhiteSpace(this.UserName) && !string.IsNullOrWhiteSpace(this.Password);
}
