using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Identity.Configurations;
using IdentityLearning.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityLearning.Identity;

public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
{
    public DbSet<ApplicationUserSession> Sessions { get; set; } = null!;
    public DbSet<BlacklistedAccessToken> BlacklistedAccessTokens { get; set; } = null!;
    public DbSet<TokenInfo> TokenInfo { get; set; } = null!; 

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUserSession>()
            .HasOne(s => s.ApplicationUser)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUserSession>()
            .HasOne(s => s.AccessToken)
            .WithOne(t => t.Session)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<TokenInfo>()
            .HasOne(t => t.Session)
            .WithOne(s => s.AccessToken)
            .HasForeignKey<TokenInfo>(t => t.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ApplyConfiguration(new ApplicationRolesConfiguration());
        builder.ApplyConfiguration(new ApplicationUsersConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
    }
}