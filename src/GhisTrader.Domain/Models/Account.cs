// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Account : EntityBase
{

    public double Balance { get; set; }
    public ICollection<AssetTransaction>? AssetTransactions { get; set; }

    // Foreign
    //public AppUser AppUserId { get; set; }
    // public AppUser AccountHolder { get; set; }
    public AppUser? AppUser { get; set; }
}
