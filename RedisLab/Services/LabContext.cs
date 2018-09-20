using Microsoft.EntityFrameworkCore;
using RedisLab.Domain;

namespace RedisLab.Services
{
    public class LabContext: DbContext
    {
        public LabContext(DbContextOptions<LabContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
