using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
