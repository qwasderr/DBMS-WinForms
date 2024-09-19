namespace Database3
{
    partial class SchemaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox txtTableName;
        private TextBox txtFieldName;
        private ComboBox cmbFieldType;
        private ListBox lstFields;
        private Button btnAddField;
        private Button btnSaveSchema;
        private Label lblTableName;
        private Label lblFieldName;
        private Label lblFieldType;
        private Label lblFieldsList;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Initialize and configure controls
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.cmbFieldType = new System.Windows.Forms.ComboBox();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.btnAddField = new System.Windows.Forms.Button();
            this.btnSaveSchema = new System.Windows.Forms.Button();

            // Initialize Labels
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.lblFieldsList = new System.Windows.Forms.Label();

            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(120, 20);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(150, 20);

            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(120, 60);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(150, 20);

            // 
            // cmbFieldType
            // 
            this.cmbFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldType.Items.AddRange(new object[] {
    "string",
    "int",
    "real",
    "time",
    "timeint",
    "char" 
});
            this.cmbFieldType.Location = new System.Drawing.Point(120, 100);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Size = new System.Drawing.Size(150, 21);

            // 
            // lstFields
            // 
            this.lstFields.Location = new System.Drawing.Point(20, 160);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(250, 150);

            // 
            // btnAddField
            // 
            this.btnAddField.Location = new System.Drawing.Point(20, 320);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(75, 23);
            this.btnAddField.Text = "Add Field";
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);

            // 
            // btnSaveSchema
            // 
            this.btnSaveSchema.Location = new System.Drawing.Point(195, 320);
            this.btnSaveSchema.Name = "btnSaveSchema";
            this.btnSaveSchema.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSchema.Text = "Save Schema";
            this.btnSaveSchema.Click += new System.EventHandler(this.btnSaveSchema_Click);

            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(20, 23);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(74, 13);
            this.lblTableName.Text = "Schema Name:";

            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(20, 63);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(63, 13);
            this.lblFieldName.Text = "Field Name:";

            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(20, 103);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(58, 13);
            this.lblFieldType.Text = "Field Type:";

            // 
            // lblFieldsList
            // 
            this.lblFieldsList.AutoSize = true;
            this.lblFieldsList.Location = new System.Drawing.Point(20, 143);
            this.lblFieldsList.Name = "lblFieldsList";
            this.lblFieldsList.Size = new System.Drawing.Size(63, 13);
            this.lblFieldsList.Text = "Fields List:";

            // 
            // SchemaForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 360);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.cmbFieldType);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.btnAddField);
            this.Controls.Add(this.btnSaveSchema);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.lblFieldType);
            this.Controls.Add(this.lblFieldsList);
            this.Name = "SchemaForm";
            this.Text = "Create Schema";
        }


        #endregion
    }
}