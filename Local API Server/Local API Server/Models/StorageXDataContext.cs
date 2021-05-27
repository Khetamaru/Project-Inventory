using Microsoft.EntityFrameworkCore;

namespace Local_API_Server.Models
{
    public class StorageXDataContext : DbContext
    {
        public StorageXDataContext(DbContextOptions<StorageXDataContext> options)
            : base(options)
        {
        }

        public DbSet<StorageXData> StorageXData { get; set; }
    }
}
