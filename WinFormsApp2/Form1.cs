namespace WinFormsApp2

{
    public partial class Form1 : Form
    {
        public string filePath = string.Empty;

        public Form1()
        {
            InitializeComponent();

        }
        public string getFilePath()
        {
            return filePath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  //browse button
        {
            filePath = string.Empty;
            var fileContent = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "txt files (*.txt)|*.txt"; //Add more file types later if you can get the statistics to work on them
                                                    //use "| All files (*.*)|*.*" as template to add more
            ofd.FilterIndex = 2;
            ofd.Multiselect = true;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = ofd.FileName;
                textBox1.Text = filePath;
                //read the contents of the file into a stream !!! Should be making a call to the main program method
                var fileStream = ofd.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //enter button
        {
            if (filePath == string.Empty)
            {
                var popUpError = new PopUpError();
                popUpError.Show();
                return;
            }
            var dataTables = new DataTables(filePath);
            dataTables.Show();
            this.Hide();
        }
    }
}