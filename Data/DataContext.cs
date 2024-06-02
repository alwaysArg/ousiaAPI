using Microsoft.EntityFrameworkCore;
using ousiaAPI.Model;

namespace ousiaAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Ply> Plys { get; set; }
    }
}
