using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class VersionLibraryContext : DbContext
    {
        public VersionLibraryContext(DbContextOptions<VersionLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<VersionLibrary> VersionLibraries { get; set; }
    }
}