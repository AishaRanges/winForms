using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Edit_Page : Form
    {
        // UI Components
        private Label[] Labels;
        private TextBox[] TextBoxes;
        private ComboBox CourseCmb, YearCmb;
        private Button SaveBtn;

        private string[] FieldNames =
        {
            "Name", "Age", "Address", "Contact Number", "Email Address",
            "Course", "Year", "Guardian/Parent", "Guardian Contact", "Hobbies", "Nickname"
        };

        public Edit_Page()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Form Settings
            this.Text = "Edit Student Profile";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Labels and Textboxes
            Labels = new Label[FieldNames.Length];
            TextBoxes = new TextBox[FieldNames.Length - 2]; // Excluding Course & Year (ComboBoxes)

            int yOffset = 20;
            for (int i = 0; i < FieldNames.Length; i++)
            {
                Labels[i] = new Label
                {
                    Text = FieldNames[i] + ":",
                    Location = new Point(20, yOffset),
                    AutoSize = true
                };
                Controls.Add(Labels[i]);

                if (i == 5) // Course Dropdown
                {
                    CourseCmb = new ComboBox
                    {
                        Location = new Point(180, yOffset - 3),
                        Width = 250,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    CourseCmb.Items.AddRange(new string[] { "ABEL", "BSBA", "BSIT", "BPA" });
                    Controls.Add(CourseCmb);
                }
                else if (i == 6) // Year Dropdown
                {
                    YearCmb = new ComboBox
                    {
                        Location = new Point(180, yOffset - 3),
                        Width = 250,
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    YearCmb.Items.AddRange(new string[] { "First", "Second", "Third", "Fourth" });
                    Controls.Add(YearCmb);
                }
                else // Normal Textboxes
                {
                    TextBoxes[i > 6 ? i - 2 : i] = new TextBox
                    {
                        Location = new Point(180, yOffset - 3),
                        Width = 250
                    };

                    // Validate Age and Contact Numbers (Numbers Only)
                    if (i == 1 || i == 3 || i == 8)
                    {
                        TextBoxes[i > 6 ? i - 2 : i].KeyPress += NumericOnly_KeyPress;
                    }

                    Controls.Add(TextBoxes[i > 6 ? i - 2 : i]);
                }

                yOffset += 35;
            }

            // Save Button
            SaveBtn = new Button
            {
                Text = "Save/Update",
                Location = new Point(180, yOffset + 10),
                Width = 120
            };
            SaveBtn.Click += SaveBtn_Click; // Attach click event
            Controls.Add(SaveBtn);
        }

        // Prevent letters in Age and Contact fields
        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are allowed in this field.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Save/Update Button Click Event
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(TextBoxes[0].Text) ||  // Name
                string.IsNullOrWhiteSpace(TextBoxes[1].Text) ||  // Age
                string.IsNullOrWhiteSpace(TextBoxes[2].Text) ||  // Address
                string.IsNullOrWhiteSpace(TextBoxes[3].Text) ||  // Contact Number
                string.IsNullOrWhiteSpace(TextBoxes[4].Text) ||  // Email
                CourseCmb.SelectedIndex == -1 ||                // Course
                YearCmb.SelectedIndex == -1 ||                  // Year
                string.IsNullOrWhiteSpace(TextBoxes[5].Text) ||  // Guardian
                string.IsNullOrWhiteSpace(TextBoxes[6].Text))    // Guardian Contact
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show success message
            MessageBox.Show("Profile successfully updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Close Edit_Page and return to Student_Page
            Student_Page studentForm = new Student_Page();
            studentForm.Show();
            this.Close();
        }
    }
}