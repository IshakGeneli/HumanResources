using HumanResources.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Contexts
{
    public class HumanResourcesDbContext : DbContext
    {
        public HumanResourcesDbContext(DbContextOptions<HumanResourcesDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permit> Permits { get; set; }
    }
}
