using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlbBlogger1.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string ADMIN_ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";
        const string AGENT_ROLE_ID = "a14bs9c0-aa65-4af8-bd17-00bd9344e575";

        var hasher = new PasswordHasher<ApplicationUser>();
          modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
          {
              Id = ADMIN_ID,
              UserName = "admin@admin.com",
              NormalizedUserName = "ADMIN@ADMIN.COM",
              Email = "admin@admin.com",
              NormalizedEmail = "ADMIN@ADMIN.COM",
              EmailConfirmed = true,
              PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
              SecurityStamp = string.Empty
          });
            
          modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
          {
              Id = ADMIN_ROLE_ID,
              Name = "admin",
              NormalizedName = "ADMIN"
          });
    }
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
    
}