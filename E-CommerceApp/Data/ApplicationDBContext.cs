using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : 
            base(dbContextOptions) 
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderItem>(x => x.HasKey(p => new { p.OrderId, p.ProductId }));

            builder.Entity<OrderItem>()
                .HasOne(u => u.Order)
                .WithMany(u => u.OrderItems)
                .HasForeignKey(p => p.OrderId);

            builder.Entity<OrderItem>()
                .HasOne(u => u.Product)
                .WithMany(u => u.OrderItems)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<OrderItem>().HasKey(x => x.OrderItemId);


            string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";
            string ROLE_ID = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6";

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            var appUser = new AppUser
            {
                Id = ADMIN_ID,
                UserName = "Admin",
                NormalizedUserName = "ADMIN"
            };

            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Admin@123");
            appUser.Email = "Admin@gmail.com";

            builder.Entity<AppUser>().HasData(appUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
