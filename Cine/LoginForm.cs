using System;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
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
            Application.Exit(); // Close the application
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Campos vacíos", "Error");
                return;
            }

            string username = userTextBox.Text;
            string password = passwordTextBox.Text;

            string hashedPassword = GetHashedPassword(password);

            bool isAuthenticated = VerifyLoginCredentials(username, hashedPassword);

            if (isAuthenticated)
            {

                MessageBox.Show("Login successful", "Success");
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error");
            }
        }

        private bool VerifyLoginCredentials(string username, string hashedPassword)
        {
            string query = "SELECT * FROM usuarios WHERE usuario = @usuario AND clave = @clave";
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source = usuariosClaves.db;"))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", username);
                        cmd.Parameters.AddWithValue("@clave", hashedPassword);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error");
                    return false;
                }
            }
        }

        private string GetHashedPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Convert each byte to a hexadecimal string
                }

                return builder.ToString();
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
