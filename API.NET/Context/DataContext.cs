using API.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace API.NET.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Cargo> Cargo { get; set; }

    public DbSet<Setor> Setor { get; set; }

    public DbSet<Colaborador> Colaborador { get; set; }

    public DbSet<Usuario> Usuario { get; set; }

}
