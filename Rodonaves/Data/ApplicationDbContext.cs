using Microsoft.EntityFrameworkCore;
using Rodonaves.Models;
using System.Collections.Generic;

namespace Rodonaves.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Unidades> Unidades { get; set; }
        public DbSet<Colaboradores> Colaboradores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Username=admin;Password=12345;Database=rodonaves;Connection Pruning Interval=1;Connection Idle Lifetime=2;Enlist=false;No Reset On Close=true;Include Error Detail=true");
        }
    }
}
