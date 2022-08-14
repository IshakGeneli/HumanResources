using HumanResources.Identity;
using HumanResources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Contexts
{
    public class HumanResourcesDbContext : IdentityDbContext<AppUser>
    {
        public HumanResourcesDbContext()
        {

        }
        public HumanResourcesDbContext(DbContextOptions<HumanResourcesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().Property(p => p.BirthDate).HasColumnType("Date");
            builder.Entity<Employee>().Property(p => p.HireDate).HasColumnType("Date");

            builder.Entity<Report>().Property(p => p.EntryDate).HasColumnType("Date");
            builder.Entity<Report>().Property(p => p.ReportDate).HasColumnType("Date");

            builder.Entity<Models.Task>().Property(p => p.CreatedDate).HasColumnType("Date");


            var user = new AppUser
            {
                Id = "f73826e9-34c2-43cd-82aa-ec181350c358",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
            };

            PasswordHasher<AppUser> hasher = new();
            user.PasswordHash = hasher.HashPassword(user, "123456");

            builder.Entity<AppUser>().HasData(user);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "18a7bbf3-014b-4426-bb7d-5e8ad5dd6df0",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "18a7bbf3-014b-4426-bb7d-5e8ad5dd6df0",
                UserId = "f73826e9-34c2-43cd-82aa-ec181350c358"
            });
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permit> Permits { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

    }
}
