// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.EntityFramework.Services;

using GhisTrader.Domain.Models;
using GhisTrader.EntityFramework.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class NonQueryDataService<T> where T : EntityBase
{
    private readonly ITraderDbContextFactory  contextFactory;

    public NonQueryDataService(ITraderDbContextFactory contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public async Task<T> Create(T entity)
    {
        using (var context = this.contextFactory.CreateAppUserDbContext())
        {
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }
    }

    public async Task<T> Update(int id, T entity)
    {
        using (var context = this.contextFactory.CreateAppUserDbContext())
        {
            entity.Id = id;

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using (var context = this.contextFactory.CreateAppUserDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }
    }
}