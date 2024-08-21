using System;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    public DbSet<EUser> Users { get; set; }
    public DbSet<EUserCredentials> UsersCredentials { get; set; }
    public DbSet<ECustomer> Customers { get; set; }
    public DbSet<EVendor> Vendors { get; set; }
}
