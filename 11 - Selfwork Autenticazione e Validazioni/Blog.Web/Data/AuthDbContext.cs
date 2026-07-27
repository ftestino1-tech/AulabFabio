using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore; 

namespace Blog.Web.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ======================================
            // 1) CREAZIONE RUOLI
            // ======================================

            var userRoleId = "111111111-1111-1111-1111-111111111111";
            var adminRoleId = "222222222-2222-2222-2222-222222222222";
            var superAdminRoleId = "333333333-3333-3333-3333-33333333333";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User", 
                    NormalizedName = "USER"
                }, 
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin", 
                    NormalizedName = "ADMIN"
                }, 
                new IdentityRole
                {
                    Id = superAdminRoleId,
                    Name = "SuperAdmin", 
                    NormalizedName = "SUPERADMIN"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles); 

            // ======================================
            // 2) CREAZIONE SUPER ADMIN
            // ======================================

            var superAdminId = "999999999-9999-9999-9999999999999";
            var superAdminEmail = "superadmin@blog.com";

            var superAdminUser = new IdentityUser
            {
                Id = superAdminId,
                UserName = superAdminEmail,
                NormalizedUserName = superAdminEmail.ToUpper(),
                Email = superAdminEmail,
                NormalizedEmail = superAdminEmail.ToUpper(), 
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            superAdminUser.PasswordHash = 
                passwordHasher.HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser); 


             // ======================================
            // 3) ASSEGNAZIONE RUOLI AL SUPER ADMIN
            // =======================================

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = superAdminId, 
                    RoleId = userRoleId
                },
                   new IdentityUserRole<string>
                {
                    UserId = superAdminId, 
                    RoleId = adminRoleId
                },
                   new IdentityUserRole<string>
                {
                    UserId = superAdminId, 
                    RoleId = superAdminRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}