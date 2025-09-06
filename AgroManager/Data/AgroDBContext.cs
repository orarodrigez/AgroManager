using Microsoft.EntityFrameworkCore;

namespace AgroManager.Data
{
    public class AgroDBContext : DbContext
    {
        public AgroDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Models.Domain.User> Users { get; set; }
        public DbSet<Models.Domain.Field> Fields { get; set; }
        public DbSet<Models.Domain.ControllerDevice> ControllerDevices { get; set; }
    }
}
