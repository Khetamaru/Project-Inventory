using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class StorageLibraryContext : DbContext
    {
        public StorageLibraryContext(DbContextOptions<StorageLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<StorageLibrary> StorageLibraries { get; set; }
    }
}