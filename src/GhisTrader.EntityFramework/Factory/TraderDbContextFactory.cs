// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.EntityFramework.Factory
{
    using GhisTrader.EntityFramework.DbContexts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TraderDbContextFactory: ITraderDbContextFactory
    {
        public AppUserDbContext CreateAppUserDbContext()
        { 
            //DbContextOptionsBuilder<AppUserDbContext> options = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            //_configureDbContext(options);

            return new AppUserDbContext( );
        }
        public object CreateOtherDbContext()
        {
            
            return "";
        }
       
    }
}
