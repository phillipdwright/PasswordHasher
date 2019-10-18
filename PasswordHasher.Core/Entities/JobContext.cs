using Microsoft.EntityFrameworkCore;

namespace PasswordHasher.Core.Entities
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
    }
}
