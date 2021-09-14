using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class ListOptionLibraryContext : DbContext
    {
        public ListOptionLibraryContext(DbContextOptions<ListOptionLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<ListOptionLibrary> ListOptionLibraries { get; set; }
    }
}
