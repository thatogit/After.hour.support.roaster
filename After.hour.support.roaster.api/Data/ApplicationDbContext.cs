using After.hour.support.roaster.api.Model;
using Microsoft.EntityFrameworkCore;

namespace After.hour.support.roaster.api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Roaster> Roasters { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }


    }
}
