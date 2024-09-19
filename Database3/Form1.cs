namespace Database3
{
    public partial class Form1 : Form
    {
        public Form1(Database database)
        {
            _database = database;
            InitializeComponent();
            InitializeDifference();
            InitializeElements();
        }
        public Form1()
        {
            InitializeComponent();
            InitializeDifference();
            InitializeElements();
            //_database = new Database();
        }

    }
}
