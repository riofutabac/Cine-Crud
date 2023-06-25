using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Cine
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cerrar la aplicación
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string usuario = userTextBox.Text.Trim();
            string clave = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Campos vacios", "Error");
                return;
            }

            string query = "SELECT * FROM usuarios WHERE usuario = @usuario AND clave = @clave";
            string connectionString = @"Data Source = usuariosClaves.db; Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);

                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            ClearFields();
                      
                            MenuCine form2 = new MenuCine();
                            form2.ShowDialog();
              
                           this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Credenciales inválidas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearFields();
                            userTextBox.Focus();
                        }
                    }
                }
            }
        }

        private void ClearFields()
        {
            userTextBox.Text = string.Empty;
            passwordTextBox.Text = string.Empty;
        }

        private void userTextBox_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void userTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                passwordTextBox.Focus();
            }
        }
    }
}
