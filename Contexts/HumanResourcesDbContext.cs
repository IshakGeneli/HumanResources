using HumanResources.Identity;
using HumanResources.Models;
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

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permit> Permits { get; set; }
    }
}
