using System;
using System.Collections.Generic;

namespace MvcProgram.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public int AccessFailedCount { get; set; }

    public string? Address { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string Discriminator { get; set; } = null!;

    public string? Email { get; set; }

    public int EmailConfirmed { get; set; }

    public int LockoutEnabled { get; set; }

    public string? LockoutEnd { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public int PhoneNumberConfirmed { get; set; }

    public string? SecurityStamp { get; set; }

    public int TwoFactorEnabled { get; set; }

    public string? UserName { get; set; }

    public int? Age { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Order> OrderReqests { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderResponses { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
