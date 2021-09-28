using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class BugLibraryContext : DbContext
    {
        public BugLibraryContext(DbContextOptions<BugLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<BugLibrary> BugLibraries { get; set; }
    }
}