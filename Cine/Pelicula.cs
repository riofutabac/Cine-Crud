using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    internal class Pelicula
    {
       
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Actores { get; set; }

        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        
        public string Director { get; set; }

        // Agrega otras propiedades y relaciones según tus necesidades
    }
}
