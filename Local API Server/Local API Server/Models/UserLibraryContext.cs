using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class UserLibraryContext : DbContext
    {
        public UserLibraryContext(DbContextOptions<UserLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<UserLibrary> UserLibraries { get; set; }
    }
}