using API.NET6.Models;
using Microsoft.EntityFrameworkCore;

namespace API.NET6.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Cargo> Cargo { get; set; }

        public DbSet<Setor> Setor { get; set; }

        public DbSet<Colaborador> Colaborador { get; set; }
    }
}
