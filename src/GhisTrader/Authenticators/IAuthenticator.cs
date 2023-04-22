// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Authenticators;

using GhisTrader.Domain;
using GhisTrader.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAuthenticator
{
    Account CurrentAccount { get; }
    bool IsLoggedIn { get; }

    event Action StateChanged;

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <param name="email">The user's email.</param>
    /// <param name="username">The user's name.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="confirmPassword">The user's confirmed password.</param>
    /// <returns>The result of the registration.</returns>
    /// <exception cref="Exception">Thrown if the registration fails.</exception>
    Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);

    /// <summary>
    /// Login to the application.
    /// </summary>
    /// <param name="username">The user's name.</param>
    /// <param name="password">The user's password.</param>
    /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
    /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
    /// <exception cref="Exception">Thrown if the login fails.</exception>
    Task Login(string username, string password);

    void Logout();
}

public class Authenticator : IAuthenticator
{
    private readonly IAuthenticationService2 authenticationService2;
    private readonly IAccountStore accountStore;

    public Authenticator(IAuthenticationService2 authenticationService2, IAccountStore accountStore)
    {
        this.authenticationService2 = authenticationService2;
        this.accountStore = accountStore;
    }

    public Account? CurrentAccount
    {
        get
        {
            return this.accountStore.CurrentAccount;
        }
        private set
        {
            this.accountStore.CurrentAccount = value;
            StateChanged?.Invoke();
        }
    }

    public bool IsLoggedIn => CurrentAccount != null;

    public event Action StateChanged;

    public async Task Login(string username, string password)
    {
        CurrentAccount = await this.authenticationService2.Login(username, password);
    }

    public void Logout()
    {
        this.CurrentAccount = null;
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
    {
        return await this.authenticationService2.Register(email, username, password, confirmPassword);
    }
}