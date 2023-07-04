using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                // Obtener los datos de la película desde los TextBox
                string nombre = nombreTextBox.Text.Trim();
                string actores = actoresTextBox.Text.Trim();
                string descripcion = descripcionTextBox.Text.Trim();
                int duracion = int.Parse(duracionTextBox.Text.Trim());
                string genero = generoTextBox.Text.Trim();
                int edad = int.Parse(edadTextBox.Text.Trim());
                string director = directorTextBox.Text.Trim();

                // Crear una nueva instancia de la clase Pelicula
                Pelicula nuevaPelicula = new Pelicula
                {
                    Nombre = nombre,
                    Actores = actores,
                    Descripcion = descripcion,
                    Duracion = duracion,
                    Genero = genero,
                    Edad = edad,
                    Director = director
                };

                // Asegurarse de que no se agregue una película duplicada
                if (peliculaManager.Exists(nuevaPelicula))
                {
                    MessageBox.Show("La película ya existe en la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Agregar la nueva película a la lista
                peliculaManager.Add(nuevaPelicula);
                List<Pelicula> peliculas = peliculaManager.GetAll();
                IComparer<Pelicula> comparador = Comparer<Pelicula>.Create((p1, p2) => string.Compare(p1.Nombre, p2.Nombre));

                // Ordenar la lista utilizando el algoritmo de inserción
                InsertionSortAlgorithm<Pelicula> insertionSort = new InsertionSortAlgorithm<Pelicula>();
                insertionSort.InsertionSort(peliculas, new PeliculaComparer());


                // Actualizar la visualización de la lista de películas
                dataGridView1.Rows.Clear();
                foreach (var pelicula in peliculas)
                {
                    dataGridView1.Rows.Add(pelicula.Nombre, pelicula.Actores, pelicula.Descripcion, pelicula.Duracion, pelicula.Genero, pelicula.Edad, pelicula.Director);
                }

                // Limpiar los TextBox
                resetTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir la película: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private class PeliculaComparer : IComparer<Pelicula>
        {
            public int Compare(Pelicula x, Pelicula y)
            {
                return string.Compare(x.Nombre, y.Nombre, StringComparison.OrdinalIgnoreCase);
            }
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

                // Crear una instancia de la clase BinarySearchAlgorithm
                BinarySearchAlgorithm<Pelicula> bs = new BinarySearchAlgorithm<Pelicula>();

                // Crear una instancia de la clase PeliculaComparer para comparar por nombre
                PeliculaComparer nombreComparer = new PeliculaComparer();

                // Crear una instancia de la clase QuickSort
                QuickSortAlgorithm qs = new QuickSortAlgorithm();

                // Ordenar la lista de películas por nombre utilizando el método Sort de QuickSort
                qs.Sort(peliculas, nombreComparer);

                // Realizar la búsqueda binaria por nombre
                Pelicula peliculaBuscada = new Pelicula { Nombre = nombre };
                int index = bs.BinarySearch(peliculas.ToArray(), peliculaBuscada, nombreComparer);

                if (index >= 0)
                {
                    // La película fue encontrada
                    peliculaBuscada = peliculas[index];

                    // Mostrar la información de la película encontrada en los TextBox correspondientes
                    nombreTextBox.Text = peliculaBuscada.Nombre;
                    actoresTextBox.Text = peliculaBuscada.Actores;
                    descripcionTextBox.Text = peliculaBuscada.Descripcion;
                    duracionTextBox.Text = peliculaBuscada.Duracion.ToString();
                    generoTextBox.Text = peliculaBuscada.Genero;
                    edadTextBox.Text = peliculaBuscada.Edad.ToString();
                    directorTextBox.Text = peliculaBuscada.Director;

                    MessageBox.Show("Película encontrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Seleccionar automáticamente la fila correspondiente en el DataGridView
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = index;
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


        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(busquedaTextBox.Text.Trim()))
                {
                    MessageBox.Show("Ingrese un nombre para realizar la búsqueda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string parametro = comboBox1.SelectedItem.ToString();
                string valorBusqueda = busquedaTextBox.Text.Trim();
                List<Pelicula> peliculas = peliculaManager.GetAll();

                switch (parametro)
                {
                    case "Nombre":
                        // Realizar la búsqueda por nombre
                        List<Pelicula> peliculasPorNombre = peliculas.Where(p => p.Nombre.ToLower() == valorBusqueda.ToLower()).ToList();
                        MostrarResultadosBusqueda(peliculasPorNombre);
                        break;
                    case "Actores":
                        // Realizar la búsqueda por actores
                        List<Pelicula> peliculasPorActores = peliculas.Where(p => p.Actores.ToLower().Contains(valorBusqueda.ToLower())).ToList();
                        MostrarResultadosBusqueda(peliculasPorActores);
                        break;
                    case "Descripción":
                        // Realizar la búsqueda por descripción
                        List<Pelicula> peliculasPorDescripcion = peliculas.Where(p => p.Descripcion.ToLower().Contains(valorBusqueda.ToLower())).ToList();
                        MostrarResultadosBusqueda(peliculasPorDescripcion);
                        break;
                    case "Duración":
                        // Realizar la búsqueda por duración
                        int duracion;
                        if (int.TryParse(valorBusqueda, out duracion))
                        {
                            List<Pelicula> peliculasPorDuracion = peliculas.Where(p => p.Duracion == duracion).ToList();
                            MostrarResultadosBusqueda(peliculasPorDuracion);
                        }
                        else
                        {
                            MessageBox.Show("Ingrese un valor numérico para realizar la búsqueda por duración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Género":
                        // Realizar la búsqueda por género
                        List<Pelicula> peliculasPorGenero = peliculas.Where(p => p.Genero.ToLower() == valorBusqueda.ToLower()).ToList();
                        MostrarResultadosBusqueda(peliculasPorGenero);
                        break;
                    case "Edad":
                        // Realizar la búsqueda por edad
                        int edad;
                        if (int.TryParse(valorBusqueda, out edad))
                        {
                            List<Pelicula> peliculasPorEdad = peliculas.Where(p => p.Edad == edad).ToList();
                            MostrarResultadosBusqueda(peliculasPorEdad);
                        }
                        else
                        {
                            MessageBox.Show("Ingrese un valor numérico para realizar la búsqueda por edad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    default:
                        MessageBox.Show("Seleccione un parámetro válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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

        private void MostrarResultadosBusqueda(List<Pelicula> peliculasEncontradas)
        {
            if (peliculasEncontradas.Count > 0)
            {
                // La(s) película(s) fue(ron) encontrada(s)
                Pelicula peliculaBuscada = peliculasEncontradas.First();

                // Mostrar la información de la primera película encontrada en los TextBox correspondientes
                nombreTextBox.Text = peliculaBuscada.Nombre;
                actoresTextBox.Text = peliculaBuscada.Actores;
                descripcionTextBox.Text = peliculaBuscada.Descripcion;
                duracionTextBox.Text = peliculaBuscada.Duracion.ToString();
                generoTextBox.Text = peliculaBuscada.Genero;
                edadTextBox.Text = peliculaBuscada.Edad.ToString();
                directorTextBox.Text = peliculaBuscada.Director;

                MessageBox.Show("Película encontrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Seleccionar automáticamente la fila correspondiente en el DataGridView
                int index = peliculaManager.GetAll().IndexOf(peliculaBuscada);
                dataGridView1.ClearSelection();
                dataGridView1.Rows[index].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = index;
            }
            else
            {
                MessageBox.Show("No se encontró ninguna película con ese criterio de búsqueda.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Informacion form1 = new Informacion();
            form1.ShowDialog();

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

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            var opcion = cmbOrdenar.SelectedIndex;
            List<Pelicula> peliculas = peliculaManager.GetAll().ToList();

            //Obtener el comparador correspondiente a la opción seleccionada
            IComparer<Pelicula> comparador;
            switch (opcion)
            {
                case 0: // Ordenar por nombre
                    comparador = Comparer<Pelicula>.Create((p1, p2) => string.Compare(p1.Nombre, p2.Nombre));
                    break;
                case 1: // Ordenar por actores
                    comparador = Comparer<Pelicula>.Create((p1, p2) => string.Compare(p1.Actores, p2.Actores));
                    break;
                case 2: // Ordenar por descripción
                    comparador = Comparer<Pelicula>.Create((p1, p2) => string.Compare(p1.Descripcion, p2.Descripcion));
                    break;
                case 3: // Ordenar por duración
                    comparador = Comparer<Pelicula>.Create((p1, p2) => p1.Duracion.CompareTo(p2.Duracion));
                    break;
                case 4: // Ordenar por género
                    comparador = Comparer<Pelicula>.Create((p1, p2) => string.Compare(p1.Genero, p2.Genero));
                    break;
                case 5: // Ordenar por edad
                    comparador = Comparer<Pelicula>.Create((p1, p2) => p1.Edad.CompareTo(p2.Edad));
                    break;
                default:
                    return; // Salir si no se selecciona una opción válida
            }


            //Llamar al método QuickSort para ordenar las películas
            QuickSortAlgorithm qs = new QuickSortAlgorithm();
            qs.Sort(peliculas, comparador);

            // Actualizar el DataGridView con los datos ordenados
            dataGridView1.Rows.Clear();
            foreach (var pelicula in peliculas)
            {
                dataGridView1.Rows.Add(pelicula.Nombre, pelicula.Actores, pelicula.Descripcion, pelicula.Duracion, pelicula.Genero, pelicula.Edad, pelicula.Director);
            }
        }
    }
}
