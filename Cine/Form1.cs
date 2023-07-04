using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cine
{
    public partial class Form1 : Form
    {
        private readonly PeliculaManager peliculaManager = new PeliculaManager();
        private List<Pelicula> peliculas;
        private int currentMovieIndex = 0;

        public Form1()
        {
            InitializeComponent();
            peliculas = peliculaManager.GetAll().ToList();
            LoadMovieData(0);
        }

        // Move to the next movie in the list
        private void siguienteButton_Click(object sender, EventArgs e)
        {
            LoadMovieData(currentMovieIndex + 1);
        }

        // Move to the previous movie in the list
        private void atrasButton_Click(object sender, EventArgs e)
        {
            LoadMovieData(currentMovieIndex - 1);
        }

        // Move to the first movie in the list
        private void inicioButton_Click(object sender, EventArgs e)
        {
            LoadMovieData(0);
        }

        // Move to the last movie in the list
        private void finButton_Click(object sender, EventArgs e)
        {
            LoadMovieData(peliculas.Count - 1);
        }

        // Load movie data into the form
        private void LoadMovieData(int index)
        {
            if (index < 0 || index >= peliculas.Count)
            {
                MessageBox.Show("No more movies in this direction.");
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
            LoadMovieData(0);
        }
    }
}
