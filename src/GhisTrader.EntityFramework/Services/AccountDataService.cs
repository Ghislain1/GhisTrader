// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.EntityFramework.Services;
    using GhisTrader.Domain.Models;
    using GhisTrader.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using GhisTrader.EntityFramework.Factory;
using Microsoft.EntityFrameworkCore;

public class AccountDataService : IAccountService
    {
        private readonly ITraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccountDataService(ITraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Account> Get(int id)
        {
            using (var context = _contextFactory.CreateAppUserDbContext())
            {
                Account entity = await context.Accounts
                    .Include(a => a.AppUser)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (var context = _contextFactory.CreateAppUserDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts
                    .Include(a => a.AppUser)
                    .Include(a => a.AssetTransactions)
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<Account> GetByEmail(string email)
        {
            using (var context = _contextFactory.CreateAppUserDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AppUser)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(a => a.AppUser.Email == email);
            }
        }

        public async Task<Account> GetByUsername(string username)
        {
            using (var context = _contextFactory.CreateAppUserDbContext())
            {
                return await context.Accounts
                    .Include(a => a.AppUser)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(a => a.AppUser.Username == username);
            }
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
 
}
