using System.Text.Json;
using System.Xml.Linq;
namespace Database3
{
    partial class Form1 : Form
    {
        private Database _database;
        private Table _selectedTable;
        private DataGridView _tableDataGridView;
        private ListBox _tableListBox;
        private TextBox _tableNameTextBox;
        private ComboBox _schemaComboBox;
        private List<DataGridViewCell> _modifiedCells = new List<DataGridViewCell>();
        private List<TextBox> _inputTextBoxes;
        private List<ComboBox> _inputComboBoxes;
        private List<DateTimePicker> _inputDatePickers;
        private List<Label> _inputLabels = new List<Label>();
        private System.ComponentModel.IContainer components = null;
        private Button _differenceButton;
        private ComboBox _tableComboBox1;
        private ComboBox _tableComboBox2;

        private void InitializeDifference()
        {
            _differenceButton = new Button
            {
                Text = "Calculate Difference",
                Location = new System.Drawing.Point(700, 100),
                Width = 200,
                Height = 30
            };
            _differenceButton.Click += DifferenceButton_Click;
            this.Controls.Add(_differenceButton);

            _tableComboBox1 = new ComboBox
            {
                Location = new System.Drawing.Point(700, 150),
                Width = 200
            };
            this.Controls.Add(_tableComboBox1);

            _tableComboBox2 = new ComboBox
            {
                Location = new System.Drawing.Point(700, 200),
                Width = 200
            };
            this.Controls.Add(_tableComboBox2);

            UpdateTableComboBoxes();
        }
        private void UpdateTableComboBoxes()
        {
            if (_database != null)
            {
                _tableComboBox1.Items.Clear();
                _tableComboBox2.Items.Clear();

                foreach (var table in _database.Tables)
                {
                    _tableComboBox1.Items.Add(table.Name);
                    _tableComboBox2.Items.Add(table.Name);
                }
            }
        }
        private void DifferenceButton_Click(object sender, EventArgs e)
        {
            var tableName1 = _tableComboBox1.SelectedItem?.ToString();
            var tableName2 = _tableComboBox2.SelectedItem?.ToString();

            if (tableName1 == null || tableName2 == null)
            {
                MessageBox.Show("Select two tables.");
                return;
            }

            var table1 = _database.GetTable(tableName1);
            var table2 = _database.GetTable(tableName2);

            if (table1 == null || table2 == null)
            {
                MessageBox.Show("One or both selected tables do not exist.");
                return;
            }

            try
            {
                var differenceTable = table1.Difference(table2);
                MessageBox.Show($"Difference table created: {differenceTable.Name}");
                
                _database.AddTable(differenceTable);
                UpdateTableComboBoxes();
                UpdateTableList();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to calculate difference: {ex.Message}");
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            ClientSize = new Size(565, 376);
            Name = "Form1";
            Text = "Form1";
           
            //Load += Form1_Load;
            ResumeLayout(false);
        }

        private void TableDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var changedCell = _tableDataGridView[e.ColumnIndex, e.RowIndex];
            if (!_modifiedCells.Contains(changedCell))
            {
                _modifiedCells.Add(changedCell);
            }
        }

        /*private bool ValidateCell(object value, Field field)
        {
            string fieldType = field.Type.ToLower();

            switch (fieldType)
            {
                case "ID":
                         
                    if (!int.TryParse(value?.ToString(), out _))
                    {
                        return false;
                    }
                    break;
                case "int":
                    
                    if (!int.TryParse(value?.ToString(), out _))
                    {
                        return false;
                    }
                    break;

                case "real":
                    
                    if (!float.TryParse(value?.ToString(), out _))
                    {
                        return false;
                    }
                    break;

                case "char":
                    
                    if (value?.ToString().Length != 1)
                    {
                        return false;
                    }
                    break;

                case "string":
                    
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                    {
                        return false;
                    }
                    break;

                case "time":
                    
                    if (!TimeSpan.TryParseExact(value?.ToString(), @"hh\:mm\:ss",
                        System.Globalization.CultureInfo.InvariantCulture, out _))
                    {
                        return false; 
                    }
                    break;

                case "timeint":
                   
                    if (TimeSpan.TryParseExact(value?.ToString(), @"hh\:mm\:ss",
                        System.Globalization.CultureInfo.InvariantCulture, out TimeSpan parsedTime))
                    {
                        TimeSpan startTime = new TimeSpan(9, 0, 0);  
                        TimeSpan endTime = new TimeSpan(17, 0, 0);   

                        if (parsedTime < startTime || parsedTime > endTime)
                        {
                            return false; 
                        }
                    }
                    else
                    {
                        return false; 
                    }
                    break;

                default:
                    return false; 
            }

            return true;
        }*/
        private bool ValidateCell(object value, Field field)
        {
            string fieldType = field.Type.ToLower();

            switch (fieldType)
            {
                case "int":
                    if (!int.TryParse(value?.ToString(), out _))
                    {
                        return false;
                    }
                    break;

                case "real":
                    if (!float.TryParse(value?.ToString(), out _))
                    {
                        return false;
                    }
                    break;

                case "char":
                    if (value?.ToString().Length != 1)
                    {
                        return false;
                    }
                    break;

                case "string":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                    {
                        return false;
                    }
                    break;

                case "time":

                    if (!TimeSpan.TryParseExact(value?.ToString(), @"hh\:mm\:ss",
                        System.Globalization.CultureInfo.InvariantCulture, out _))
                    {
                        return false;
                    }
                    break;

                case "timeint":
                    if (TimeSpan.TryParseExact(value?.ToString(), @"hh\:mm\:ss",
                        System.Globalization.CultureInfo.InvariantCulture, out TimeSpan timeValue))
                    {
                        if (field.LowerBound.HasValue && field.UpperBound.HasValue)
                        {
                            if (timeValue < field.LowerBound.Value || timeValue > field.UpperBound.Value)
                            {
                                MessageBox.Show($"Time value for field '{field.Name}' must be between {field.LowerBound.Value} and {field.UpperBound.Value}.");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Invalid time format in field '{field.Name}'. Please enter time in HH:MM format.");
                        return false;
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }

        private void ResetModifiedCells()
        {
            _modifiedCells.Clear();
        }
        #endregion

        private void DeleteTableButton_Click(object sender, EventArgs e)
        {
            if (_selectedTable != null)
            {
                var result = MessageBox.Show("Are you sure?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _database.DeleteTable(_selectedTable.Name);
                    _selectedTable = null; 

                    UpdateTableList();
                    _tableDataGridView.Rows.Clear();
                    _tableDataGridView.Columns.Clear();
                    MessageBox.Show("Success.");
                }
            }
            else
            {
                MessageBox.Show("Choose the Table.");
            }
        }
        private void CreateSchemaButton_Click(object sender, EventArgs e)
        {
            
            using (var createSchemaForm = new SchemaForm(_database))
            {
                if (createSchemaForm.ShowDialog() == DialogResult.OK)
                {
                    var newSchema = createSchemaForm.NewSchema;
                    _database.AddSchema(newSchema);
                    PopulateSchemaComboBox();
                    
                }
            }
        }
        private void PopulateSchemaComboBox()
        {
            _schemaComboBox.Items.Clear();

            foreach (var schema in _database.Schemas)
            {
                if (schema != null)
                {
                    _schemaComboBox.Items.Add(schema.Name);
                }
            }

            if (_schemaComboBox.Items.Count > 0)
            {
                _schemaComboBox.SelectedIndex = 0;
            }
        }
        private void InitializeElements()
        {
            _inputTextBoxes = new List<TextBox>();
            _inputComboBoxes = new List<ComboBox>();
            _inputDatePickers = new List<DateTimePicker>();
            _inputLabels = new List<Label>();


            Button createSchemaButton = new Button
            {
                Text = "Create Schema",
                Location = new Point(540, 20),
                Width = 200,
                Height = 30
            };
            createSchemaButton.Click += CreateSchemaButton_Click;
            this.Controls.Add(createSchemaButton);

            Button deleteTableButton = new Button
            {
                Text = "Delete the table",
                Location = new Point(400, 270),
                Width = 200,
                Height = 30
            };
            deleteTableButton.Click += DeleteTableButton_Click;
            this.Controls.Add(deleteTableButton);

            Button saveDatabaseButton = new Button()
            {
                Text = "Save Database",
                Height = 30,
                Location = new System.Drawing.Point(300, 270)
            };
            saveDatabaseButton.Click += SaveDatabaseButton_Click;
            this.Controls.Add(saveDatabaseButton);

            _schemaComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(20, 20),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            PopulateSchemaComboBox();
            _schemaComboBox.SelectedIndexChanged += SchemaComboBox_SelectedIndexChanged;
            this.Controls.Add(_schemaComboBox);



            _tableNameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(230, 20),
                Width = 200
            };
            this.Controls.Add(_tableNameTextBox);

            Button createTableButton = new Button
            {
                Text = "Create Table",
                Location = new System.Drawing.Point(440, 20),
                Width = 100,
                Height = 30
            };
            createTableButton.Click += (sender, args) =>
            {
                try
                {
                    string tableName = _tableNameTextBox.Text;
                    string selectedSchema = _schemaComboBox.SelectedItem?.ToString();

                    if (!string.IsNullOrWhiteSpace(tableName) && !string.IsNullOrEmpty(selectedSchema))
                    {
                        Schema schema = GetSchemaByName(selectedSchema);
                        if (schema != null)
                        {
                            _database.CreateTable(tableName, schema);
                            UpdateTableList();
                            UpdateTableComboBoxes();
                            MessageBox.Show("Table created successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Invalid schema selected.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter a table name and select a schema.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
            };
            this.Controls.Add(createTableButton);


            _tableListBox = new ListBox
            {
                Location = new System.Drawing.Point(20, 60),
                Width = 200,
                Height = 200
            };
            _tableListBox.SelectedIndexChanged += (sender, args) =>
            {
                if (_tableListBox.SelectedItem != null)
                {
                    _selectedTable = _database.GetTable(_tableListBox.SelectedItem.ToString());
                    UpdateTableRows();
                }
            };
            this.Controls.Add(_tableListBox);
            UpdateTableList();


            _tableDataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(230, 60),
                Width = 400,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(_tableDataGridView);
            _tableDataGridView.CellValueChanged += TableDataGridView_CellValueChanged;

            Button addRowButton = new Button
            {
                Text = "Add Row",
                Location = new System.Drawing.Point(20, 270),
                Width = 100,
                Height = 30
            };
            addRowButton.Click += (sender, args) =>
            {
                if (_selectedTable != null)
                {
                    try
                    {
                        List<Value> values = new List<Value>();
                        int textBoxIndex = 0;
                        bool isValid = true;

                        foreach (var field in _selectedTable.Schema.Fields)
                        {
                            if (field.Name == "ID")
                            {
                                continue; 
                            }

                            object inputValue = null;

                            if (textBoxIndex < _inputTextBoxes.Count)
                            {
                                var textBox = _inputTextBoxes[textBoxIndex];
                                string inputText = textBox.Text;

                                switch (field.Type.ToLower())
                                {
                                    case "time":
                                        
                                        if (TimeSpan.TryParseExact(inputText, @"hh\:mm\:ss",
                                            System.Globalization.CultureInfo.InvariantCulture, out TimeSpan parsedTime))
                                        {
                                            inputValue = parsedTime;
                                            values.Add(new Value(parsedTime));
                                        }
                                        else
                                        {
                                            isValid = false;
                                            MessageBox.Show($"Invalid time format in field '{field.Name}'. Enter time in format HH:MM:SS.");
                                            return;
                                        }
                                        break;

                                    case "timeint":
                                       
                                        if (TimeSpan.TryParseExact(inputText, @"hh\:mm\:ss",
                                            System.Globalization.CultureInfo.InvariantCulture, out TimeSpan parsedTimeInt))
                                        {
                                            TimeSpan? startTime = field.LowerBound;  
                                            TimeSpan? endTime = field.UpperBound;   

                                            if (parsedTimeInt < startTime || parsedTimeInt > endTime)
                                            {
                                                isValid = false;
                                                MessageBox.Show($"Invalid timeint value in field '{field.Name}'. Enter a time between {field.LowerBound} and {field.UpperBound}.");
                                                return;
                                            }
                                            inputValue = parsedTimeInt;
                                            values.Add(new Value(parsedTimeInt));
                                        }
                                        else
                                        {
                                            isValid = false;
                                            MessageBox.Show($"Invalid time format in field '{field.Name}'. Enter time in format HH:MM:SS.");
                                            return;
                                        }
                                        break;

                                    case "string":
                                        
                                        inputValue = inputText;
                                        values.Add(new Value(inputText));
                                        break;

                                    case "char":
                                        
                                        if (inputText.Length == 1)
                                        {
                                            inputValue = inputText[0];
                                            values.Add(new Value(inputText[0]));
                                        }
                                        else
                                        {
                                            isValid = false;
                                            MessageBox.Show($"Invalid char value in field '{field.Name}'. Enter a single character.");
                                            return;
                                        }
                                        break;

                                    case "int":
                                        
                                        if (int.TryParse(inputText, out int intValue))
                                        {
                                            inputValue = intValue;
                                            values.Add(new Value(intValue));
                                        }
                                        else
                                        {
                                            isValid = false;
                                            MessageBox.Show($"Invalid integer value in field '{field.Name}'.");
                                            return;
                                        }
                                        break;

                                    case "real":
                                        
                                        if (double.TryParse(inputText, out double doubleValue))
                                        {
                                            inputValue = doubleValue;
                                            values.Add(new Value(doubleValue));
                                        }
                                        else
                                        {
                                            isValid = false;
                                            MessageBox.Show($"Invalid real number value in field '{field.Name}'.");
                                            return;
                                        }
                                        break;

                                    default:
                                        isValid = false;
                                        MessageBox.Show($"Unknown field type '{field.Type}' in field '{field.Name}'.");
                                        return;
                                }

                               
                                if (!ValidateCell(inputValue, field))
                                {
                                    isValid = false;
                                    MessageBox.Show($"Invalid value in field '{field.Name}'.");
                                    return;
                                }

                                textBoxIndex++;
                            }
                        }

                        
                        if (isValid)
                        {
                            var newRow = new Row(values);
                            _selectedTable.AddRow(newRow);
                            UpdateTableRows(); 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to add row: {ex.Message}");
                    }
                }
            };




            this.Controls.Add(addRowButton);


            Button deleteRowButton = new Button
            {
                Text = "Delete Row",
                Location = new System.Drawing.Point(130, 270),
                Width = 100,
                Height = 30
            };
            deleteRowButton.Click += (sender, args) =>
            {
                if (_selectedTable != null && _tableDataGridView.SelectedRows.Count > 0)
                {
                    int selectedIndex = _tableDataGridView.SelectedRows[0].Index;
                    _selectedTable.DeleteRow(selectedIndex);
                    UpdateTableRows();
                }
            };
            this.Controls.Add(deleteRowButton);
        }


        private void SchemaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTableRows();
        }

        private Schema GetSchemaByName(string schemaName)
        {
           
            foreach (var schema in _database.Schemas)
            {
                if (schema.Name == schemaName)
                {
                    return schema;
                }
            }
            return null;
        }

        private void UpdateTableList()
        {

            _tableListBox.Items.Clear();
            foreach (var table in _database.Tables)
            {
                _tableListBox.Items.Add(table.Name);
            }
        }

        private void UpdateTableRows()
        {
            if (_selectedTable != null)
            {
               
                _tableDataGridView.Columns.Clear();
                foreach (var control in _inputTextBoxes)
                {
                    this.Controls.Remove(control);
                }
                foreach (var control in _inputComboBoxes)
                {
                    this.Controls.Remove(control);
                }
                foreach (var control in _inputLabels)
                {
                    this.Controls.Remove(control);
                }

                _inputTextBoxes.Clear();
                _inputComboBoxes.Clear();
                _inputLabels.Clear();

                int yOffset = 300;

                
                var idColumn = new DataGridViewTextBoxColumn
                {
                    HeaderText = "ID",
                    Name = "ID",
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                _tableDataGridView.Columns.Add(idColumn);

               
                foreach (var field in _selectedTable.Schema.Fields)
                {
                    if (field.Name != "ID")
                    {
                        _tableDataGridView.Columns.Add(field.Name, field.Name);

                       
                        var label = new Label
                        {
                            Text = field.Name,
                            Location = new System.Drawing.Point(20, yOffset),
                            Width = 100
                        };
                        _inputLabels.Add(label);
                        this.Controls.Add(label);

                        
                        Control inputControl;
                        switch (field.Type.ToLower())
                        {
                            case "time":
                                
                                var timeTextBox = new TextBox
                                {
                                    Location = new System.Drawing.Point(130, yOffset),
                                    Width = 200,
                                    PlaceholderText = "HH:MM:SS"
                                };
                                _inputTextBoxes.Add(timeTextBox);
                                inputControl = timeTextBox;
                                break;

                            case "timeint":
                                
                                var timeIntTextBox = new TextBox
                                {
                                    Location = new System.Drawing.Point(130, yOffset),
                                    Width = 200,
                                    PlaceholderText = "HH:MM:SS"
                                };
                                _inputTextBoxes.Add(timeIntTextBox);
                                inputControl = timeIntTextBox;
                                break;

                            case "string":
                            case "int":
                            case "real":
                            case "char":
                                var textBox = new TextBox
                                {
                                    Location = new System.Drawing.Point(130, yOffset),
                                    Width = 200
                                };
                                _inputTextBoxes.Add(textBox);
                                inputControl = textBox;
                                break;

                            default:
                                continue;
                        }

                        this.Controls.Add(inputControl);
                        yOffset += 30;
                    }
                }

                
                _tableDataGridView.Rows.Clear();
                foreach (var row in _selectedTable.Rows)
                {
                    var rowValues = new object[row.Values.Count];
                    /*for (int i = 0; i < row.Values.Count; i++)
                    {
                       
                        if (_selectedTable.Schema.Fields[i].Type.ToLower() == "timeint")
                        {
                            var timeIntValue = (TimeSpan)row.Values[i].FieldValue;
                            rowValues[i] = timeIntValue.ToString(@"hh\:mm\:ss");
                        }
                        else
                        {
                            rowValues[i] = row.Values[i].FieldValue;
                        }
                    }*/
                    for (int i = 0; i < row.Values.Count; i++)
                    {
                        //var fieldType = _selectedTable.Schema.Fields[i].Type.ToLower();
                        var fieldValue = row.Values[i].FieldValue;

                        
                           
                            if (fieldValue is TimeSpan timeSpanValue)
                            {
                                rowValues[i] = timeSpanValue.ToString(@"hh\:mm\:ss");
                            }
                            else
                            {
                                rowValues[i] = fieldValue; 
                            }
                        
                        
                    }

                    _tableDataGridView.Rows.Add(rowValues);
                }
            }
        }

        private void SaveDatabaseButton_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            if (_selectedTable != null)
            {
                string tableName = _selectedTable.Schema.Name;

                foreach (DataGridViewRow row in _tableDataGridView.Rows)
                {
                    if (row.IsNewRow) continue;

                    var idCell = row.Cells["ID"].Value;
                    int rowId;

                    if (idCell is JsonElement jsonElement)
                    {
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                        {
                            rowId = jsonElement.GetInt32();
                        }
                        else
                        {
                            MessageBox.Show("Invalid ID value.");
                            continue;
                        }
                    }
                    else
                    {
                        rowId = Convert.ToInt32(idCell);
                    }

                    var existingRow = _selectedTable.Rows.FirstOrDefault(r => r.Values[0].ToInt() == rowId);

                    if (existingRow != null)
                    {
                        List<Value> updatedValues = new List<Value>();

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string columnName = _tableDataGridView.Columns[cell.ColumnIndex].Name;
                            object cellValue = cell.Value;
                           
                                var field = _selectedTable.Schema.Fields.FirstOrDefault(f => f.Name == columnName);

                                if (field == null || !ValidateCell(cellValue, field))
                                {
                                    isValid = false;
                                    cell.Style.BackColor = Color.Red;
                                }
                                else
                                {
                                    cell.Style.BackColor = Color.White;
                                    updatedValues.Add(new Value(cellValue));
                                }
                            
                        }

                        if (isValid)
                        {
                            existingRow.Values = updatedValues;
                        }
                    }
                }
            }

            if (isValid)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Database files (*.json)|*.json|All files (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _database.SaveToDisk(saveFileDialog.FileName);
                    MessageBox.Show("Database saved.");
                }
            }
            else
            {
                MessageBox.Show("Invalid data.");
            }
        }
    }

   
}
