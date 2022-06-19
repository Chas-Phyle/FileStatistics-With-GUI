namespace WinFormsApp2
{
    public partial class DataTables : Form
    {
        public DataTables(string path)
        {
            InitializeComponent(path);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form form = new Exitprompt();
            form.Show();
            //Application.Exit();
        }
    }
}
