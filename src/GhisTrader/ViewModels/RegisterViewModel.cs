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
using GhisTrader.Domain.Models;
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
    private readonly IRenavigator? loginRenavigator;
    private readonly IRenavigator? registerRenavigator;
    private readonly IAuthenticator? authenticator;
    private string? email;
    private string? username;
    private string? password;
    private string? errorMessage;
    private string? confirmPassword;
    public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator, IRenavigator loginRenavigator)
    {

        this.authenticator = authenticator;
        this.registerRenavigator = registerRenavigator;
        this.loginRenavigator = loginRenavigator;

        this.RegisterCommand = new RelayCommand(this.ExecuteRegister, () => this.CanRegister).ObservesProperty(() => this.CanRegister);
        this.ViewLoginCommand = new RelayCommand(this.ExecuteViewLogin).ObservesProperty(() => this.CanRegister);

        this.FillDemoDataAsync();
    }

    private async Task FillDemoDataAsync()
    {
        Random random = new Random();
        await Task.Delay(1000);
        if (this.CanRegister)
        {
            return;
        }
        this.Username = "Ghis" + random.Next(1, 100);
        this.Password = "1234";
        this.ConfirmPassword = this.Password;
        this.Email = $"{this.Username}@FakeEmail.com";
    }

    private void ExecuteViewLogin()
    {
        this.loginRenavigator?.Renavigate();
    }

    private async void ExecuteRegister()
    {
        await Task.Run(() => this.DoRegistryAsync());

    }

    private async Task DoRegistryAsync()
    {
        this.ErrorMessage = string.Empty;

        try
        {
            RegistrationResult registrationResult = await this.authenticator!.Register(this.Email!, this.Username!, this.Password!,
                 this.ConfirmPassword!);

            switch (registrationResult)
            {
                case RegistrationResult.Success:
                    this.registerRenavigator?.Renavigate();
                    break;
                case RegistrationResult.PasswordsDoNotMatch:
                    this.ErrorMessage = "Password does not match confirm password.";
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    this.ErrorMessage = "An account for this email already exists.";
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    this.ErrorMessage = "An account for this username already exists.";
                    break;
                default:
                    this.ErrorMessage = "Registration failed.";
                    break;
            }
        }
        catch (Exception ex)
        {
            this.ErrorMessage = "Registration failed." + ex.Message;
        }
    }
    public string? ErrorMessage
    {
        get => this.errorMessage;
        set => this.InvokePropertyChanged(this.PropertyChanged, ref this.errorMessage, value);

    }
    public string? Email
    {
        get => this.email;
        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.email, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanRegister));
            }
        }
    }

    public string? Username
    {
        get => this.username;
        set
        {
            if (this.InvokePropertyChanged(this.PropertyChanged, ref this.username, value))
            {
                this.InvokePropertyChanged(this.PropertyChanged, nameof(this.CanRegister));
            }
        }


    }
    public string? Password
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



    public event PropertyChangedEventHandler? PropertyChanged;

    public string? ConfirmPassword
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