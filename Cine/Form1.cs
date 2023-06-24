using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cine
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = @"Data Source=DESKTOP-Q4AVP4D;Initial Catalog=loginCine;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the application
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = userTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Usuarios_Claves WHERE usuario = @usuario AND contraseña = @contraseña";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", username);
                        cmd.Parameters.AddWithValue("@contraseña", password);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                ClearFields();

                                MenuCine form2 = new MenuCine();
                                form2.ShowDialog();
                                Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ClearFields();
                                userTextBox.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
