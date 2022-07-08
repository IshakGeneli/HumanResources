using HumanResources.Identity;
using HumanResources.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Contexts
{
    public class HumanResourcesDbContext : IdentityDbContext<AppUser>
    {
        public HumanResourcesDbContext(DbContextOptions<HumanResourcesDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permit> Permits { get; set; }
    }
}
