using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class LogLibraryContext : DbContext
    {
        public LogLibraryContext(DbContextOptions<LogLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<LogLibrary> LogLibraries { get; set; }
    }
}