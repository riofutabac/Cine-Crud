using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cine
{
    internal class PeliculaGateway
    {
        private PeliculaContexto _dbContext = new PeliculaContexto();

        public bool Add(Pelicula pelicula)
        {
            _dbContext.Peliculas.Add(pelicula);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Pelicula> GetAll()
        {
            return _dbContext.Peliculas.ToList();
        }

        public bool Update(Pelicula pelicula)
        {
            var data = _dbContext.Peliculas.FirstOrDefault(p => p.Id == pelicula.Id);
            if (data == null)
            {
                return false;
            }
             data.Nombre = pelicula.Nombre;
             data.Actores = pelicula.Actores;
             data.Director = pelicula.Director;
             data.Genero = pelicula.Genero;
             data.Edad = pelicula.Edad;
             data.Duracion = pelicula.Duracion;
             data.Descripcion = pelicula.Descripcion;

            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var pelicula = _dbContext.Peliculas.FirstOrDefault(p => p.Id == id);
            if (pelicula == null)
            {
                return false;
            }
            _dbContext.Peliculas.Remove(pelicula);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
