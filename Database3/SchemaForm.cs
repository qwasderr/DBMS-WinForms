﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database3
{
    public partial class SchemaForm : Form
    {
        public Schema NewSchema { get; private set; }
        private Database _database;
        public SchemaForm(Database database)
        {
            InitializeComponent();
            NewSchema = null;
            _database = database;
        }
        private void btnAddField_Click(object sender, EventArgs e)
        {
            
            string fieldName = txtFieldName.Text;
            string fieldType = cmbFieldType.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(fieldName) || fieldType == null)
            {
                MessageBox.Show("Please provide both field name and type.");
                return;
            }

            lstFields.Items.Add($"{fieldName} ({fieldType})");
            txtFieldName.Clear();
            cmbFieldType.SelectedIndex = -1;
        }

        private void btnSaveSchema_Click(object sender, EventArgs e)
        {
            string tableName = txtTableName.Text;
            if (string.IsNullOrWhiteSpace(tableName) || lstFields.Items.Count == 0)
            {
                MessageBox.Show("Please provide a schema name and at least one field.");
                return;
            }

           
            var fields = new List<Field>();
            fields.Add(new Field("ID", "int"));
            foreach (var item in lstFields.Items)
            {
                var parts = item.ToString().Split(' ');
                if (parts.Length == 2)
                {
                    fields.Add(new Field(parts[0], parts[1].Trim('(', ')')));
                }
            }

            var newSchema = new Schema(fields, tableName);

            _database.AddSchema(newSchema);

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Database files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Database File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _database.SaveToDisk(saveFileDialog.FileName);
                    MessageBox.Show("Database saved.");

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
