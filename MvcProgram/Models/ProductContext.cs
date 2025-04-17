using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcProgram.Models;

public partial class ProductContext : DbContext
{
    public ProductContext()
    {
    
    }

    public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
    {
    }
    public DbSet<Models.Product> Product{get ; set ; }
    public DbSet<Models.User> User{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
