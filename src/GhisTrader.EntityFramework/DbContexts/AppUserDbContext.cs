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

        // public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AssetTransaction> AssetTransactions { get; set; }
        public DbSet<Asset> Assets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=appUsers.db");
            // optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //AppUser
            var appUser = this.Create<AppUser>(builder);
            appUser.HasOne(x => x.Account)
                   .WithOne(x => x.AppUser)
                   .HasForeignKey<Account>(x => x.AppUserId);




            // Account
            var account = this.Create<Account>(builder);
            //account.HasOne(x => x.AppUser)
            //    .WithOne(x=> x.Account)
            //    .HasForeignKey<AppUser>(x=> x.AccountId);

            // AssetTransaction
            var assetTransaction = this.Create<AssetTransaction>(builder);
            assetTransaction.HasOne(x => x.Asset);

            // Asset
            var asset = this.Create<Asset>(builder);
            //asset.HasOne(x => x.Asset);

            base.OnModelCreating(builder);
        }
        private EntityTypeBuilder<T> Create<T>(ModelBuilder modelBuilder) where T : EntityBase
        {
            var entity = modelBuilder.Entity<T>();
            entity.HasKey(x => x.Id);
            // var tableName2 = $"{nameof(T)}s";
            var tableName = typeof(T).Name +"s";
            entity.ToTable(tableName);
            return entity;

        }
    }
}
