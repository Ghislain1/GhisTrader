// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.EntityFramework.DbContexts
{
    using GhisTrader.Domain.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    // TODO@GZe: later public  class AppUserDbContext : IdentityDbContext<AppUser>
    public class AppUserDbContext : DbContext
    {
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options) { }

        //  Second way to create Your context( best way for WPF)
        public AppUserDbContext() : base() { }

        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<Account> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=appUsers.db");
            // optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //AppUser
            var appUser = this.Create<AppUser>(builder);
            appUser.HasOne(user => user.Account);
                  

            // Account
            var account = this.Create<Account>(builder);
            account.HasOne(x => x.AppUser);





            base.OnModelCreating(builder);
        }
        private EntityTypeBuilder<TEntity> Create<TEntity>(ModelBuilder modelBuilder) where TEntity : EntityBase
        {
            var entity = modelBuilder.Entity<TEntity>();
            entity.HasKey(x => x.Id);
            entity.ToTable($"{nameof(TEntity)}s");
            return entity;

        }
    }
}
