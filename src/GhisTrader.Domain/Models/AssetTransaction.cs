// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    public class AssetTransaction : EntityBase
    {
        public Account Account { get; set; }
        public bool IsPurchase { get; set; }
        public Asset Asset { get; set; }
        public int Shares { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}