using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlbBlogger1.Models;

namespace AlbBlogger1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(l => l.Post)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Like>(b =>
            {
                b.HasOne(l => l.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(l => l.PostId)
                    .OnDelete(DeleteBehavior.NoAction); // Change cascade delete to NoAction

                b.HasOne(l => l.User)
                    .WithMany(u => u.Likes)
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.NoAction); // Change cascade delete to NoAction
            });

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ADMIN_ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";

            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed roles
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = "a14bs9c0-aa65-4af8-bd17-00bd9344e575",
                    Name = "NormalUser",
                    NormalizedName = "NORMALUSER"
                });

            // Seed users
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
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

            // Seed user roles
            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole
                {
                    UserId = ADMIN_ID,
                    RoleId = ADMIN_ROLE_ID
                });

            // Configure entity relationships, etc.
            // Example: modelBuilder.Entity<Entity>().HasOne(e => e.OtherEntity).WithMany().HasForeignKey(e => e.OtherEntityId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        
        // public DbSet<AlbBlogger1.Models.PostViewModel> PostViewModel { get; set; } = default!;
        

        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
