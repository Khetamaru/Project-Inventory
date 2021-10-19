using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class SaveContext : DbContext
    {
        public SaveContext(DbContextOptions<UserLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<UserLibrary> Save { get; set; }
    }
}