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

public class AppUser : EntityBase
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string ?PasswordHash { get; set; }
    public DateTime DatedJoined { get; set; }

    //Foreign
    //public int AccountId { get; set; }         
    public Account? Account { get; set; }
    
}