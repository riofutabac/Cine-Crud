using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cine
{
    public partial class Informacion : Form
    {
        private readonly PeliculaManager peliculaManager = new PeliculaManager();
        private List<Pelicula> peliculas;
        private int currentMovieIndex = 0;

        public Informacion()
        {
            InitializeComponent();
            peliculas = peliculaManager.GetAll().ToList();
            cargarPeliculas(0);
        }


        private void siguienteButton_Click(object sender, EventArgs e)
        {
            cargarPeliculas(currentMovieIndex + 1);
        }

     
        private void atrasButton_Click(object sender, EventArgs e)
        {
            cargarPeliculas(currentMovieIndex - 1);
        }

    
        private void inicioButton_Click(object sender, EventArgs e)
        {
            cargarPeliculas(0);
        }

     
        private void finButton_Click(object sender, EventArgs e)
        {
            cargarPeliculas(peliculas.Count - 1);
        }


        private void cargarPeliculas(int index)
        {
            if (index < 0 || index >= peliculas.Count)
            {
                MessageBox.Show("No hay peliculas.");
                return;
            }

            currentMovieIndex = index;

            Pelicula pelicula = peliculas[currentMovieIndex];

            nombreTextBox.Text = pelicula.Nombre;
            actoresTextBox.Text = pelicula.Actores;
            descripcionTextBox.Text = pelicula.Descripcion;
            duracionTextBox.Text = pelicula.Duracion.ToString();
            generoTextBox.Text = pelicula.Genero;
            edadTextBox.Text = pelicula.Edad.ToString();
            directorTextBox.Text = pelicula.Director;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cargarPeliculas(0);
        }
    }
}
