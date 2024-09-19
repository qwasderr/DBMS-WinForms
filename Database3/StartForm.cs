using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database3
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Select Action";
            this.Size = new Size(300, 200);

            Button createDatabaseButton = new Button
            {
                Text = "Create Database",
                Location = new Point(50, 50),
                Width = 200,
                Height = 40
            };
            createDatabaseButton.Click += CreateDatabaseButton_Click;
            this.Controls.Add(createDatabaseButton);

            Button loadDatabaseButton = new Button
            {
                Text = "Upload Database",
                Location = new Point(50, 100),
                Width = 200,
                Height = 40
            };
            loadDatabaseButton.Click += LoadDatabaseButton_Click;
            this.Controls.Add(loadDatabaseButton);
        }

        private void CreateDatabaseButton_Click(object sender, EventArgs e)
        {
            using (var inputDialog = new InputDialog("Enter Database Name"))
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    string databaseName = inputDialog.InputText;

                    if (!string.IsNullOrEmpty(databaseName))
                    {
                        if (!DatabaseExists(databaseName))
                        {
                            Database _database = new Database(databaseName);
                            var mainForm = new Form1(_database);
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Database with this name already exists.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Database name can't be empty.");
                    }
                }
            }
        }
        private bool DatabaseExists(string databaseName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseName + ".db");
            return File.Exists(filePath);
        }
        private void LoadDatabaseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Database files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var database = new Database("LoadedDatabase");
                database.LoadFromDisk(openFileDialog.FileName);

                var mainForm = new Form1(database);
                mainForm.Show();
                this.Hide();
            }
        }
    }

    public class InputDialog : Form
    {
        private TextBox textBox;
        private Button okButton;
        private Button cancelButton;

        public string InputText { get { return textBox.Text; } }

        public InputDialog(string prompt)
        {
            this.Text = prompt;

            textBox = new TextBox { Left = 20, Top = 20, Width = 200 };
            okButton = new Button { Text = "OK", Left = 130, Width = 90, Top = 50, DialogResult = DialogResult.OK };
            cancelButton = new Button { Text = "Cancel", Left = 30, Width = 90, Top = 50, DialogResult = DialogResult.Cancel };

            okButton.Click += (sender, e) => { this.Close(); };
            cancelButton.Click += (sender, e) => { this.Close(); };

            this.Controls.Add(textBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;

            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(250, 100);
        }
    }


}
