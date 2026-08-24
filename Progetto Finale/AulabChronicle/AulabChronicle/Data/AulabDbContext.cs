using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AulabChronicle.Models.Domain;

namespace AulabChronicle.Data
{
    public class AulabDbContext : IdentityDbContext<IdentityUser>
    {
        public AulabDbContext(DbContextOptions<AulabDbContext> options) : base(options) {}
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CareerRequest> CareerRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            var adminRoleId = "a71bdca4-500b-4bd4-9a40-4ce084883d6a";
            var revisorRoleId = "c577002b-a010-449e-990c-99c0d10c1d1a";
            var writerRoleId = "b845423f-422d-4235-9f6b-76f2d22d2f2d";
            
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                { 
                    Id = adminRoleId, 
                    Name = "Admin", 
                    NormalizedName = "ADMIN" 
                },
                new IdentityRole 
                { 
                    Id = revisorRoleId, 
                    Name = "Revisor", 
                    NormalizedName = "REVISOR" 
                },
                new IdentityRole 
                { 
                    Id = writerRoleId, 
                    Name = "Writer", 
                    NormalizedName = "WRITER" 
                }
            );

            // Seed Admin User  
            var adminUserId = "b1234567-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin", 
                Email = "admin@admin.com", 
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "Admin123!");

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            // Assign Admin Role to Admin User 
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "politica"},
                new Category { Id = 2, Name = "economia"},
                new Category { Id = 3, Name = "food&drink"},
                new Category { Id = 4, Name = "sport"},
                new Category { Id = 5, Name = "intrattenimento"},
                new Category { Id = 6, Name = "tech"}
            );

            modelBuilder.Entity<Article>()
                .Property(a => a.CategoryId)
                .IsRequired(false);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}