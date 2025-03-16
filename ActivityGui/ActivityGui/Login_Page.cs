using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using BCrypt.Net;
using System.Data.SqlClient;

namespace WindowsFormsApp
{
    public partial class Login_Page : Form
    {
        private int loginAttempts = 0;
        private const int MaxAttempts = 5;

        private Label UsernameLbl, PasswordLbl, ResetLinkLbl;
        private TextBox UsernameTxt, PasswordTxt;
        private Button LoginBtn;

        public Login_Page()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Login Page";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            UsernameLbl = new Label { Text = "Username:", Location = new Point(20, 20), AutoSize = true };
            Controls.Add(UsernameLbl);

            UsernameTxt = new TextBox { Location = new Point(120, 20), Width = 150 };
            Controls.Add(UsernameTxt);

            PasswordLbl = new Label { Text = "Password:", Location = new Point(20, 60), AutoSize = true };
            Controls.Add(PasswordLbl);

            PasswordTxt = new TextBox { Location = new Point(120, 60), Width = 150, PasswordChar = '*' };
            Controls.Add(PasswordTxt);

            LoginBtn = new Button { Text = "Login", Location = new Point(120, 100), Width = 80 };
            LoginBtn.Click += LoginBtn_Click;
            Controls.Add(LoginBtn);

            ResetLinkLbl = new Label
            {
                Text = "Forgot Password?",
                Location = new Point(120, 140),
                ForeColor = Color.Blue,
                Cursor = Cursors.Hand,
                Visible = false,
                AutoSize = true
            };
            ResetLinkLbl.Click += ResetLinkLbl_Click;
            Controls.Add(ResetLinkLbl);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTxt.Text.Trim();
            string password = PasswordTxt.Text.Trim();

            if (loginAttempts >= MaxAttempts)
            {
                MessageBox.Show("Too many failed attempts! Please reset your password.", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetLinkLbl.Visible = true;
                return;
            }

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Student_Page studentForm = new Student_Page();
                studentForm.Show();
                this.Hide();
            }
            else
            {
                loginAttempts++;

                if (loginAttempts == MaxAttempts)
                {
                    ResetLinkLbl.Visible = true;
                }

                MessageBox.Show("Username or password is incorrect!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabase"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection string is missing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT password FROM user WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string storedHash = result.ToString();
                            return BCrypt.Net.BCrypt.Verify(password, storedHash); // Secure password check
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }


        private void ResetLinkLbl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reset password functionality is not implemented yet.", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
