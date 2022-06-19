namespace WinFormsApp2
{
    public partial class Exitprompt : Form
    {
        public Exitprompt()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//used to restart program
        {
            Form form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)//used to leave
        {
            Application.Exit();

        }
    }
}
