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
public class RegisterViewModel : INotifyPropertyChanged
{
    public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator, IRenavigator loginRenavigator)
    {
        //ErrorMessageViewModel = new MessageViewModel();

        // TODO
        // RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
        //ViewLoginCommand = new RenavigateCommand(loginRenavigator);

        this.RegisterCommand = new RelayCommand(this.ExecuteRegister, () => this.CanRegister).ObservesProperty(() => this.CanRegister);
        this.ViewLoginCommand = new RelayCommand(this.ExecuteViewLogin).ObservesProperty(() => this.CanRegister);
    }

    private void ExecuteViewLogin()
    {
        throw new NotImplementedException();
    }

    private void ExecuteRegister()
    {
        throw new NotImplementedException();
    }

    private string email;
    public string Email
    {
        get
        {
            return this.email;
        }
        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.email, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanRegister));
            }
        }
    }

    private string username;
    private string password;
    public string Username
    {
        get
        {
            return this.username;
        }

        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.username, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanRegister));
            }
        }


    }


    public string Password
    {
        get => this.password;
        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.password, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanRegister));
            }
        }


    }

    private string confirmPassword;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string ConfirmPassword
    {
        get
        {
            return this.confirmPassword;
        }
        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.confirmPassword, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanRegister));
            }
        }

    }

    public bool CanRegister => !string.IsNullOrEmpty(Email) &&
        !string.IsNullOrEmpty(Username) &&
        !string.IsNullOrEmpty(Password) &&
        !string.IsNullOrEmpty(ConfirmPassword);

    public ICommand RegisterCommand { get; }

    public ICommand ViewLoginCommand { get; }

    //public MessageViewModel ErrorMessageViewModel { get; }

    //public string ErrorMessage
    //{
    //    set => ErrorMessageViewModel.Message = value;
    //}




}