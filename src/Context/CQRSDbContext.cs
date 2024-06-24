using CQRSApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace CQRSApplication.Context
{
    public class CQRSDbContext : DbContext
    {
        public CQRSDbContext(DbContextOptions<CQRSDbContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }

        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vendor>()
                .HasMany(v => v.Products)
                .WithOne()
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Vendor>()
                    .HasOne(v => v.User)
                    .WithOne()
                    .HasForeignKey<Vendor>(v => v.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>(entity =>
                    {
                        entity.HasOne(u => u.userCredentials)
                                .WithOne()
                                .HasForeignKey<User>(u => u.UserCredentialsId)
                                .OnDelete(DeleteBehavior.Cascade);
                        entity.HasOne(u => u.shippingAddress)
                                .WithOne()
                                .HasForeignKey<User>(u => u.ShippingAddressId)
                                .OnDelete(DeleteBehavior.SetNull);
                        entity.HasOne(u => u.Cart)
                                .WithOne(c => c.Customer)
                                .HasForeignKey<User>(u => u.CartId)
                                .OnDelete(DeleteBehavior.SetNull);
                    });

            builder.Entity<Cart>(entity =>
                    {
                        entity.HasOne(c => c.Customer)
                                .WithOne(u => u.Cart)
                                .HasForeignKey<Cart>(c => c.CustomerId)
                                .OnDelete(DeleteBehavior.SetNull);

                        entity.HasMany(c => c.Items)
                                .WithOne(ci => ci.Cart)
                                .HasForeignKey(ci => ci.CartId)
                                .OnDelete(DeleteBehavior.Cascade);
                    });

            builder.Entity<UserCredentials>(entity =>
                    {
                        entity.HasIndex(e => e.Email).IsUnique();
                        entity.HasIndex(e => e.UserName).IsUnique();
                    });
            // // Cart to CartItem relationship
            // builder.Entity<Cart>()
            //     .HasMany(c => c.Items)
            //     .WithOne(ci => ci.Cart)
            //     .HasForeignKey(ci => ci.CartId);

            // // Cart to User relationship
            // builder.Entity<Cart>()
            //     .HasOne(c => c.Customer)
            //     .WithOne(u => u.Cart)
            //     .HasForeignKey<Cart>(c => c.CustomerId);
            //Admin User credentials.
            var adminRole = new UserCredentials
            {
                Id = Guid.NewGuid(),
                Email = "admin@admin.com",
                UserName = "admin",
                Password = "admin@123#",
                Role = RoleType.SuperAdmin
            };
            //Admin User Data.
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "9843804968",
                Address = "",
                ImageUrl = "",
                UserCredentialsId = adminRole.Id
            };
            builder.Entity<UserCredentials>().HasData(adminRole);
            builder.Entity<User>().HasData(adminUser);
        }
    }
}
