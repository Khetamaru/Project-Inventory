using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class StorageLibraryXCustomListLibraryContext : DbContext
    {
        public StorageLibraryXCustomListLibraryContext(DbContextOptions<StorageLibraryXCustomListLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<StorageLibraryXCustomListLibrary> StorageLibrariesXCustomListLibraries { get; set; }
    }
}