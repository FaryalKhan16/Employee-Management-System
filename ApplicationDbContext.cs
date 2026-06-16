using Microsoft.EntityFrameworkCore;

namespace UniversityManagementSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Login> Logins { get; set; }
    }
}
