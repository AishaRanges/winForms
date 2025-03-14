using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Login_Page : Form
    {
        // Mock credentials
        private const string MockUsername = "admin";
        private const string MockPassword = "password123";
        private int loginAttempts = 0;
        private const int MaxAttempts = 5;

        // UI Components
        private Label UsernameLbl, PasswordLbl, ResetLinkLbl;
        private TextBox UsernameTxt, PasswordTxt;
        private Button LoginBtn;

        public Login_Page()
        {
            InitializeComponent(); // Required for Windows Forms
            InitializeCustomComponents(); // Custom UI method
        }

        private void InitializeCustomComponents()
        {
            // Form Settings
            this.Text = "Login Page";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Username Label
            UsernameLbl = new Label
            {
                Text = "Username:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            Controls.Add(UsernameLbl);

            // Username TextBox
            UsernameTxt = new TextBox
            {
                Location = new Point(120, 20),
                Width = 150
            };
            Controls.Add(UsernameTxt);

            // Password Label
            PasswordLbl = new Label
            {
                Text = "Password:",
                Location = new Point(20, 60),
                AutoSize = true
            };
            Controls.Add(PasswordLbl);

            // Password TextBox
            PasswordTxt = new TextBox
            {
                Location = new Point(120, 60),
                Width = 150,
                PasswordChar = '*' // Hide password input
            };
            Controls.Add(PasswordTxt);

            // Login Button
            LoginBtn = new Button
            {
                Text = "Login",
                Location = new Point(120, 100),
                Width = 80
            };
            LoginBtn.Click += LoginBtn_Click; // Attach event handler
            Controls.Add(LoginBtn);

            // Reset Password Link (Initially Hidden)
            ResetLinkLbl = new Label
            {
                Text = "Forgot Password?",
                Location = new Point(120, 140),
                ForeColor = Color.Blue,
                Cursor = Cursors.Hand,
                Visible = false,
                AutoSize = true
            };
            ResetLinkLbl.Click += ResetLinkLbl_Click; // Handle click event
            Controls.Add(ResetLinkLbl);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Text;

            if (loginAttempts >= MaxAttempts)
            {
                MessageBox.Show("Too many failed attempts! Please reset your password.", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetLinkLbl.Visible = true; // Show reset password link
                return;
            }

            if (username == MockUsername && password == MockPassword)
            {
                MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Student Form
                Student_Page studentForm = new Student_Page();
                studentForm.Show();
                this.Hide(); // Hide login form
            }
            else
            {
                loginAttempts++;

                if (username != MockUsername && password != MockPassword)
                    MessageBox.Show("Both username and password are incorrect!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (username != MockUsername)
                    MessageBox.Show("Username is incorrect!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Password is incorrect!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (loginAttempts == MaxAttempts)
                {
                    ResetLinkLbl.Visible = true; // Show reset password label after 5 attempts
                }
            }
        }

        private void ResetLinkLbl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reset password functionality is not implemented yet.", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}