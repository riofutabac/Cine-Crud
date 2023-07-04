using System.Collections.Generic;

namespace Cine
{
    internal class PeliculaManager
    {
        private PeliculaGateway _peliculaGateway = new PeliculaGateway();

        public bool Add(Pelicula pelicula)
        {
            return _peliculaGateway.Add(pelicula);
        }

        public List<Pelicula> GetAll()
        {
            return _peliculaGateway.GetAll();
        }

        public bool Update(Pelicula pelicula)
        {
            return _peliculaGateway.Update(pelicula);
        }

        public bool Delete(int id)
        {
            return _peliculaGateway.Delete(id);
        }
        public bool Exists(Pelicula pelicula)
        {
            List<Pelicula> peliculas = GetAll();

            foreach (Pelicula p in peliculas)
            {
                if (p.Nombre.ToLower() == pelicula.Nombre.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
