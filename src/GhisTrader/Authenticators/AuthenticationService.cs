// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Authenticators;
using GhisTrader.Domain.Exceptions;
using GhisTrader.Domain.Models;
using GhisTrader.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class AuthenticationService2 : IAuthenticationService2
{
    private readonly IAccountService accountService;
    private readonly IPasswordHasher<AppUser> passwordHasher;

    public AuthenticationService2(IAccountService accountService, IPasswordHasher<AppUser> passwordHasher)
    {
        this.accountService = accountService;
        this.passwordHasher = passwordHasher;
    }

    public async Task<Account> Login(string username, string password)
    {
        Account storedAccount = await this.accountService.GetByUsername(username);

        if (storedAccount == null)
        {
            throw new UserNotFoundException(username);
        }

        PasswordVerificationResult passwordResult = this.passwordHasher.VerifyHashedPassword(storedAccount.AppUser, storedAccount.AppUser.PasswordHash, password);

        if (passwordResult != PasswordVerificationResult.Success)
        {
            throw new InvalidPasswordException(username, password);
        }

        return storedAccount;
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
    {
        RegistrationResult result = RegistrationResult.Success;

        if (password != confirmPassword)
        {
            result = RegistrationResult.PasswordsDoNotMatch;
        }

        Account emailAccount = await this.accountService.GetByEmail(email);
        if (emailAccount != null)
        {
            result = RegistrationResult.EmailAlreadyExists;
        }

        Account usernameAccount = await this.accountService.GetByUsername(username);
        if (usernameAccount != null)
        {
            result = RegistrationResult.UsernameAlreadyExists;
        }

        if (result == RegistrationResult.Success)
        {
            AppUser user = new AppUser()
            {
                Email = email,
                Username = username,

                DatedJoined = DateTime.Now
            };
            string hashedPassword = this.passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;


            Account account = new Account()
            {
                AppUser = user,
                Balance = 500
            };

            await this.accountService.Create(account);
        }

        return result;
    }
}