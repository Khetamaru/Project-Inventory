using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class DataLibraryContext : DbContext
    {
        public DataLibraryContext(DbContextOptions<DataLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<DataLibrary> DataLibraries { get; set; }
    }
}
