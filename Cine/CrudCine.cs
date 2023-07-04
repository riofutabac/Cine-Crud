using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cine
{
    public partial class CrudCine : Form
    {

        PeliculaManager peliculaManager = new PeliculaManager();

        public CrudCine()
        {
            InitializeComponent();

        }

        private void CrudCine_Load(object sender, EventArgs e)
        {
            try
            {
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadData()
        {
            PeliculaManager peliculaManager = new PeliculaManager();// 
            var peliculas = peliculaManager.GetAll();
            dataGridView1.Rows.Clear();
            foreach (var pelicula in peliculas)
            {
                dataGridView1.Rows.Add(pelicula.Nombre, pelicula.Actores, pelicula.Descripcion, pelicula.Duracion, pelicula.Genero, pelicula.Edad, pelicula.Director);
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {

            try
            {
                // Verificar si los campos están vacíos
                if (string.IsNullOrWhiteSpace(nombreTextBox.Text) ||
                string.IsNullOrWhiteSpace(actoresTextBox.Text) ||
                string.IsNullOrWhiteSpace(descripcionTextBox.Text) ||
                string.IsNullOrWhiteSpace(duracionTextBox.Text) ||
                string.IsNullOrWhiteSpace(generoTextBox.Text) ||
                string.IsNullOrWhiteSpace(edadTextBox.Text) ||
                string.IsNullOrWhiteSpace(directorTextBox.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                Pelicula pelicula = new Pelicula();
                pelicula.Nombre = nombreTextBox.Text;
                pelicula.Actores = actoresTextBox.Text;
                pelicula.Descripcion = descripcionTextBox.Text;
                pelicula.Duracion = Convert.ToInt32(duracionTextBox.Text);
                pelicula.Genero = generoTextBox.Text;
                pelicula.Edad = Convert.ToInt32(edadTextBox.Text);
                pelicula.Director = directorTextBox.Text;

                if (peliculaManager.Add(pelicula))
                {
                    MessageBox.Show("Pelicula guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    resetTextBox();

                }
                else
                {
                    MessageBox.Show("Pelicula no guardada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void editarButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una película para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string nombre = selectedRow.Cells[0].Value.ToString();

                Pelicula peliculaOriginal = peliculaManager.GetAll().FirstOrDefault(p => p.Nombre == nombre);
                if (peliculaOriginal != null)
                {
                    Pelicula peliculaModificada = new Pelicula();
                    peliculaModificada.Id = peliculaOriginal.Id;
                    peliculaModificada.Nombre = nombreTextBox.Text;
                    peliculaModificada.Actores = actoresTextBox.Text;
                    peliculaModificada.Descripcion = descripcionTextBox.Text;
                    peliculaModificada.Duracion = Convert.ToInt32(duracionTextBox.Text);
                    peliculaModificada.Genero = generoTextBox.Text;
                    peliculaModificada.Edad = Convert.ToInt32(edadTextBox.Text);
                    peliculaModificada.Director = directorTextBox.Text;

                    if (PeliculaEquals(peliculaOriginal, peliculaModificada))
                    {
                        MessageBox.Show("No se realizaron cambios en la película.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (peliculaManager.Update(peliculaModificada))
                    {
                        MessageBox.Show("Película modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                        resetTextBox();

                    }
                    else
                    {
                        MessageBox.Show("Error al modificar la película.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool PeliculaEquals(Pelicula pelicula1, Pelicula pelicula2)
        {
            return pelicula1.Nombre == pelicula2.Nombre &&
                   pelicula1.Actores == pelicula2.Actores &&
                   pelicula1.Descripcion == pelicula2.Descripcion &&
                   pelicula1.Duracion == pelicula2.Duracion &&
                   pelicula1.Genero == pelicula2.Genero &&
                   pelicula1.Edad == pelicula2.Edad &&
                   pelicula1.Director == pelicula2.Director;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string nombre = selectedRow.Cells[0].Value.ToString();

                Pelicula pelicula = peliculaManager.GetAll().FirstOrDefault(p => p.Nombre == nombre);
                if (pelicula != null)
                {
                    nombreTextBox.Text = pelicula.Nombre;
                    actoresTextBox.Text = pelicula.Actores;
                    descripcionTextBox.Text = pelicula.Descripcion;
                    duracionTextBox.Text = pelicula.Duracion.ToString();
                    generoTextBox.Text = pelicula.Genero;
                    edadTextBox.Text = pelicula.Edad.ToString();
                    directorTextBox.Text = pelicula.Director;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una película para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string nombre = selectedRow.Cells[0].Value.ToString();

                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar la película?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Pelicula pelicula = peliculaManager.GetAll().FirstOrDefault(p => p.Nombre == nombre);
                    int id = pelicula.Id;

                    if (peliculaManager.Delete(id))
                    {
                        MessageBox.Show("Película eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                        resetTextBox();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la película.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetTextBox()
        {
            nombreTextBox.Text = string.Empty;
            actoresTextBox.Text = string.Empty;
            descripcionTextBox.Text = string.Empty;
            duracionTextBox.Text = string.Empty;
            generoTextBox.Text = string.Empty;
            edadTextBox.Text = string.Empty;
            directorTextBox.Text = string.Empty;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(busquedaTextBox.Text.Trim()))
                {
                    MessageBox.Show("Ingrese un nombre para realizar la búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombre = busquedaTextBox.Text.Trim();
                List<Pelicula> peliculas = peliculaManager.GetAll();

                // Realizar la búsqueda de la película por nombre
                Pelicula peliculaEncontrada = peliculas.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

                if (peliculaEncontrada != null)
                {
                    // Mostrar la información de la película encontrada en los TextBox correspondientes
                    nombreTextBox.Text = peliculaEncontrada.Nombre;
                    actoresTextBox.Text = peliculaEncontrada.Actores;
                    descripcionTextBox.Text = peliculaEncontrada.Descripcion;
                    duracionTextBox.Text = peliculaEncontrada.Duracion.ToString();
                    generoTextBox.Text = peliculaEncontrada.Genero;
                    edadTextBox.Text = peliculaEncontrada.Edad.ToString();
                    directorTextBox.Text = peliculaEncontrada.Director;

                    MessageBox.Show("Película encontrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Seleccionar automáticamente la fila correspondiente en el DataGridView
                    dataGridView1.ClearSelection();
                    int rowIndex = dataGridView1.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(row => row.Cells[0].Value.ToString().Equals(nombre, StringComparison.OrdinalIgnoreCase))?
                        .Index ?? -1;

                    if (rowIndex >= 0)
                    {
                        dataGridView1.Rows[rowIndex].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
                    }
                }


                else
                {
                    MessageBox.Show("No se encontró ninguna película con ese nombre.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                busquedaTextBox.Focus();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            busquedaTextBox.Text = string.Empty;
        }

        private void busquedaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


/*
        private void busquedaTextBox_TextChanged(object sender, EventArgs e)
        {
            searchData(busquedaTextBox.Text);
        }
        public void searchData(string valueToFind)
        {
            string searchQuery = "SELECT * FROM Peliculas WHERE Nombre LIKE '%" + valueToFind + "%'";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(searchQuery, peliculaManager.GetConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
*/
    }
}
