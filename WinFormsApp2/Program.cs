namespace WinFormsApp2
{
    public static class Program
    {
        private static string currentPath;
        [STAThread] //used for communication with COM components in OS
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());



        }
        public static void setPath(string pathLocation)
        {
            Program.currentPath = pathLocation;
        }
        public static String getPath()
        {
            return currentPath;
        }

        /// <summary>
        /// This method takes in the string of a document and cleans up the string to remove special characters. After which it counts each occurance of a word and sorts them into an aray
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>A 2D array with words in column 0 and their occurances in column 1</returns>
        public static string[,] fileStatistics(string directory)
        {
            string wholeDocument;
            if (!directory.Contains(".txt")) //Checks to make sure the given document is a .txt file
            {
                Application.Run(new PopUpError());
            }
            wholeDocument = File.ReadAllText(directory);
            
            var vs2 = wholeDocument.Split(" ");    //Splits the document on all Spaces
            if(vs2.Length ==1)                     //If the doucment does not contain any spaces it will split on another character
            {
                vs2 = wholeDocument.Split("\n");

            }
            vs2 = FileCleaner(vs2);
            var numberOfWords = vs2.Length;
            var query = vs2.GroupBy(r => r).Where(r => (r != null)).Select(grp => new
            {
                Value = grp.Key,
                Count = grp.Count()
            });

            var counter = 0;
            string[] word = new string[numberOfWords];
            int[] numberOfOcurances = new int[numberOfWords];
            foreach (var i in query)
            {
                word[counter] = i.Value.ToString();
                numberOfOcurances[counter] = i.Count;
                counter++;
            }
            word = word.Where(x => !string.IsNullOrEmpty(x)).ToArray();             //used to remove any null or empty values
            numberOfOcurances = numberOfOcurances.Where(x => (x != 0)).ToArray();   //used to remove anything that = 0 neither the top or bottom command should change anything but its a precaution
            counter = 0;

            Array.Sort(numberOfOcurances, word);
            string[] numberOfOcurancesString = new string[word.Length]; //need to convert numberOfOcurances to string form to pass it out of the method !!can't use object
            foreach (var i in numberOfOcurances)
            {
                numberOfOcurancesString[counter] = i.ToString();
                counter++;
            }
            var final = new String[word.Length, 2];
            counter = 0;
            foreach (var i in word)
            {
                final[counter, 0] = i;
                counter++;
            }
            counter = 0;
            foreach (var i in numberOfOcurancesString)
            {
                final[counter, 1] = i;
                counter++;
            }
            return final;
        }

        /// <summary>
        /// A method that takes in an array and removes any special characters from the array. The array will also put every character into lower case. The special characters that are removed include: ",",".","-", "—", @"""", "\"",";","!","`","?", "`", "“", "”", "‘", ":", @"\r",@"\n",@"\t". 
        /// </summary>
        /// <param name="arrayThatHasIt"></param>
        /// <returns>A 1D array</returns>
        public static string[] FileCleaner(string[] arrayThatHasIt)     //used to clean up an array that has a lot of excess characters
        {
            string[] stringToRemove = {",",".","-", "—", @"""", "\"",";","!","`","?", "`", "“", "”", "‘", ":", @"\r"
            ,@"\n",@"\t"};      //everything to remove from the document
            string[] result;
            int x = 0;
            int y = 0;
            foreach (var i in arrayThatHasIt)
            {
                foreach (var j in stringToRemove)
                {
                    string temp = arrayThatHasIt[x];
                    double tempInt;
                    bool itsANumber= double.TryParse(temp, out tempInt);    //checks if the string is a number or char
                    if (temp.Contains(stringToRemove[y]))
                    {
                        if(itsANumber)                                      //if its a number it should  be left alone and sent back without removing the decimal
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
            result = arrayThatHasIt;
            return result;
        }
    }
}