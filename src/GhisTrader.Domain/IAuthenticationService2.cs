// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Domain;

using GhisTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Why? 2 because Microsoft get his own IAuthenticationService
public interface IAuthenticationService2
{
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
    /// Get an account for a user's credentials.
    /// </summary>
    /// <param name="username">The user's name.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>The account for the user.</returns>
    /// <exception cref="UserNotFoundException">Thrown if the user does not exist.</exception>
    /// <exception cref="InvalidPasswordException">Thrown if the password is invalid.</exception>
    /// <exception cref="Exception">Thrown if the login fails.</exception>
    Task<Account> Login(string username, string password);
}