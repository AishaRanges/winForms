using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Student_Page : Form
    {
        // UI Components
        private PictureBox StudentPicture;
        private Button AddImageBtn, ChangeImageBtn, EditBtn;
        private Label[] Labels;
        private TextBox[] TextBoxes;
        private string[] FieldNames =
        {
            "Name", "Age", "Address", "Contact Number", "Email Address",
            "Course and Year", "Parent/Guardian", "Parent/Guardian Contact Number",
            "Hobbies", "Nickname"
        };

        public Student_Page()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Form Settings
            this.Text = "Student Page";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // PictureBox for Student Image
            StudentPicture = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(StudentPicture);

            // Buttons for Image Actions (Non-functional for now)
            AddImageBtn = new Button
            {
                Text = "Add Image",
                Location = new Point(160, 40),
                Width = 100
            };
            Controls.Add(AddImageBtn);

            ChangeImageBtn = new Button
            {
                Text = "Change Image",
                Location = new Point(160, 80),
                Width = 100
            };
            Controls.Add(ChangeImageBtn);

            // Labels and Textboxes for Student Details
            Labels = new Label[FieldNames.Length];
            TextBoxes = new TextBox[FieldNames.Length];

            int yOffset = 150;
            for (int i = 0; i < FieldNames.Length; i++)
            {
                Labels[i] = new Label
                {
                    Text = FieldNames[i] + ":",
                    Location = new Point(20, yOffset),
                    AutoSize = true
                };
                Controls.Add(Labels[i]);

                TextBoxes[i] = new TextBox
                {
                    Location = new Point(180, yOffset - 3),
                    Width = 250,
                    ReadOnly = true // Read-only unless edited
                };
                Controls.Add(TextBoxes[i]);

                yOffset += 35;
            }

            // Edit/Update Button
            EditBtn = new Button
            {
                Text = "Edit/Update",
                Location = new Point(180, yOffset + 10),
                Width = 100
            };
            EditBtn.Click += EditBtn_Click; // Attach click event
            Controls.Add(EditBtn);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Edit_Page editForm = new Edit_Page();
            editForm.Show();
            this.Hide(); // Hide current form
        }
    }
}
