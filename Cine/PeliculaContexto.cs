using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cine
{
    internal class PeliculaContexto : DbContext
    {
        // Agrega tus DbSets y configuraciones de entidades aquí
        public DbSet<Pelicula> Peliculas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=peliculas2023.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }
    }
}
