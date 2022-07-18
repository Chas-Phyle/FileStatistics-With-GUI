using MySql.Data.MySqlClient;
using MySQLDataAccess.DataModels;
using WinFormsApp2;


namespace FileStatistics
{
    public class Program
    {
        
        List<WordOccurencesModel> words;
        [STAThread] //used for communication with COM components in OS
        public static void Main()
        {

            MySQLDataAccess.DataAccess.MySQLDataAccess db = new();
            var dbName = "Server=127.0.0.1;Port=3306;database=filestatistics;user id=root;password=8520123789";
            var connection = new MySqlConnection(dbName);
            connection.Open();



            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            connection.Close();


        }
        private static string currentPath;
        public static async Task InsertData(int id, string word, int occurance, MySqlConnection connection)     //used to insert data in the database
        {
            string sql = $"insert into test (id , word, occurance) values ({id},'{word}',{occurance});";

            MySqlCommand cmd = new MySqlCommand(sql, connection);

            await cmd.ExecuteNonQueryAsync();           //must run as await!!!
        }
        public static MySqlDataReader GetAllData(MySqlConnection connection) //returns in desc order by occurance
        {
            string sql = "select * from test order by occurance desc;";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            var test = cmd.ExecuteReader();
            return test;
        }
        public static async Task DeleteData(MySqlConnection connection)     //used to delete all records
        {
            MySqlCommand cmd = new MySqlCommand("delete from test WHERE id <=100000;", connection); //removes 100000 records after the table has been generated for the display
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            connection.Close();
        }

        public static void setPath(string pathLocation)
        {
            currentPath = pathLocation;
        }
        public static string getPath()
        {
            return currentPath;
        }

        /// <summary>
        /// This method takes in the string of a document and cleans up the string to remove special characters. After which it counts each occurance of a word and sorts them into an aray
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>A 2D array with words in column 0 and their occurances in column 1</returns>
        public static void fileStatistics(string directory)
        {
            var dbName = "Server=127.0.0.1;Port=3306;database=filestatistics;user id=root;password=8520123789";
            var connection = new MySqlConnection(dbName);
            connection.Open();
            string wholeDocument;
            if (!directory.Contains(".txt")) //Checks to make sure the given document is a .txt file
            {
                Application.Run(new PopUpError());
            }
            wholeDocument = File.ReadAllText(directory);

            List<string> vs2 = new List<string>(wholeDocument.Split(" "));      //use list for easy splits later
                    //Splits the document on all Spaces
            //if (vs2.Count == 1)                 //If the doucment does not contain any spaces it will split on another character
            //{
            //    vs2.Add(wholeDocument.Split("\n"));

            //}
            var counter = 0;
            //foreach(var i in vs2)         Look into ways to expand the list
            //{

            //    var line = i.Split("\t");
            //    vs2[counter] = line[0];
            //    vs2.Add(line[1]);
            //    counter++;
            //}
            
            vs2 = FileCleaner(vs2);
            var numberOfWords = vs2.Count;
            var query = vs2.GroupBy(r => r).Where(r => r != null).Select(grp => new
            {
                Value = grp.Key,
                Count = grp.Count()
            });

            counter = 1;
            string[] word = new string[numberOfWords];
            int[] numberOfOcurances = new int[numberOfWords];
            foreach (var i in query)
            {
                InsertData(counter, i.Value.ToString(), i.Count, connection);
                
                //word[counter] = i.Value.ToString();
                //numberOfOcurances[counter] = i.Count;
                //counter++;
            }
            //word = word.Where(x => !string.IsNullOrEmpty(x)).ToArray();             //used to remove any null or empty values
            //numberOfOcurances = numberOfOcurances.Where(x => x != 0).ToArray();   //used to remove anything that = 0 neither the top or bottom command should change anything but its a precaution
            //counter = 0;

            //Array.Sort(numberOfOcurances, word);
            //string[] numberOfOcurancesString = new string[word.Length]; //need to convert numberOfOcurances to string form to pass it out of the method !!can't use object
            //foreach (var i in numberOfOcurances)
            //{
            //    numberOfOcurancesString[counter] = i.ToString();
            //    counter++;
            //}
            //var final = new string[word.Length, 2];
            //counter = 0;
            //foreach (var i in word)
            //{
            //    final[counter, 0] = i;
            //    counter++;
            //}
            //counter = 0;
            //foreach (var i in numberOfOcurancesString)
            //{
            //    final[counter, 1] = i;
            //    counter++;
            //}
            connection.Close();
            //return final;
            
        }

        /// <summary>
        /// A method that takes in an array and removes any special characters from the array. The array will also put every character into lower case. The special characters that are removed include: ",",".","-", "—", @"""", "\"",";","!","`","?", "`", "“", "”", "‘", ":", @"\r",@"\n",@"\t". 
        /// </summary>
        /// <param name="arrayThatHasIt"></param>
        /// <returns>A 1D array</returns>
        public static List<string> FileCleaner(List<string> listThatHasIt)     //used to clean up an array that has a lot of excess characters
        {
            var arrayThatHasIt = listThatHasIt.ToArray();
            string[] stringToRemove = {",",".","-", "—", @"""", "\"",";","!","`","?", "`", "“", "”", "‘", ":", @"\r"
            ,@"\n",@"\t"};      //everything to remove from the document
            
            int x = 0;
            int y = 0;
            foreach (var i in arrayThatHasIt)
            {
                foreach (var j in stringToRemove)
                {
                    string temp = arrayThatHasIt[x];
                    double tempInt;
                    bool itsANumber = double.TryParse(temp, out tempInt);    //checks if the string is a number or char
                    if (temp.Contains(stringToRemove[y]))
                    {
                        if (itsANumber)                                      //if its a number it should  be left alone and sent back without removing the decimal
                        {
                            arrayThatHasIt[x] = temp;
                            break;
                        }
                        string[] vs3 = temp.Split(stringToRemove[y]);
                        if (vs3[0] == stringToRemove[y])
                        {
                            arrayThatHasIt[x] = vs3[1];
                        }
                        else
                        {
                            arrayThatHasIt[x] = vs3[0];
                        }
                    }

                    y++;
                }
                x++;
                y = 0;
            }
            arrayThatHasIt = arrayThatHasIt.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();    //used to remove an null or white space
            arrayThatHasIt = arrayThatHasIt.Select(x => x.ToLowerInvariant()).ToArray();            //used to put everything into lower case
            List<string> result = new List<string>(arrayThatHasIt);
            return result;
        }
    }

    internal class WebApplication
    {
    }
}