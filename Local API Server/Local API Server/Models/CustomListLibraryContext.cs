using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class CustomListLibraryContext : DbContext
    {
        public CustomListLibraryContext(DbContextOptions<CustomListLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<CustomListLibrary> CustomListLibraries { get; set; }
    }
}
