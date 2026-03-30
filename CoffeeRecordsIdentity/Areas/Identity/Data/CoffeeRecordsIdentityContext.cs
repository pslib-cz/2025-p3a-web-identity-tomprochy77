using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CoffeeRecordsIdentity.Data;

public class CoffeeRecordsIdentityContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<CoffeeCup> Cups { get; set; }
    public CoffeeRecordsIdentityContext(DbContextOptions<CoffeeRecordsIdentityContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var adminRoleId = "admin-role-id";
        var adminUserId = "admin-user-id";
        var hasher = new PasswordHasher<ApplicationUser>();
        var adminUser = new ApplicationUser
        
        {   
            Id = adminUserId,
            UserName = "admin@test.com",
            NormalizedUserName = "ADMIN@TEST.COM",
            Email = "admin@test.com",
            NormalizedEmail = "ADMIN@TEST.COM",
            EmailConfirmed = true,
            Name = "Administrator",
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        });

        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin1234");

        builder.Entity<ApplicationUser>().HasData(adminUser);

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminUserId
        });

        

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }
}


