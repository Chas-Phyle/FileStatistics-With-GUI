using System;
using System.IO;


namespace WinFormsApp2
{
    internal static class Program
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
        public static string[,] fileStatistics(string directory)
        {
            string wholeDocument;
            if (!directory.Contains(".txt")) //Checks to make sure the given document is a .txt file
            {
                Application.Run(new PopUpError());
            }
            wholeDocument = File.ReadAllText(directory);
            //using (StreamReader sr = File.OpenText(directory))  //Outputs everything in the file to a string
            //{
            //   while ((wholeDocument = sr.ReadLine()) != null)
            //    {
              //      wholeDocument = sr.ReadLine();

                //}
            //}
            var vs2 = wholeDocument.Split(" ");    //Splits the document on all Spaces

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
                word[counter] = i.Value;
                numberOfOcurances[counter] = i.Count;
                counter++;
            }
            word = word.Where(x => !string.IsNullOrEmpty(x)).ToArray();             //used to remove any null or empty values
            numberOfOcurances = numberOfOcurances.Where(x => (x != 0)).ToArray();   //used to remove anything that = 0 neither the top or bottom command should change anything but its a precaution
            counter = 0;
            
            Array.Sort(numberOfOcurances, word);
            string[] numberOfOcurancesString = new string[word.Length]; //need to convert numberOfOcurances to string form to pass it out of the method !!can't use object
            foreach(var i in numberOfOcurances)
            {
               numberOfOcurancesString[counter] = i.ToString() ;
                counter++;
            }
            var final = new String[word.Length,2];
            counter = 0;
            foreach(var i in word)
            {
                final[counter,0] = i;
                counter++;
            }
            counter = 0;
            foreach(var i in numberOfOcurancesString)
            {
                final[counter,1] = i;
                counter++;
            }
           //creating a test file and writing to it
           //using (StreamWriter sw = File.CreateText(directory + "test.txt"))    //need to parse out the example.txt in the directory protion and just get to the folder
                //maybe get rid of this whole section
           // {
            //    for(int i = 0; i < word.Length; i++)
              //  {
                //    sw.WriteLine($"{word[i]}\t\t{numberOfOcurances[i]}\n");

               // }
            //}
            //Open the file to read from **this is the important one!***
            return final;
        }
       
        static string[] FileCleaner(string[] arrayThatHasIt)     //used to clean up an array that has a lot of excess characters
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
                    if (temp.Contains(stringToRemove[y]))
                    {
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
                }x++;
                y = 0;
            }
            arrayThatHasIt = arrayThatHasIt.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();    //used to remove an null or white space
            arrayThatHasIt = arrayThatHasIt.Select(x => x.ToLowerInvariant()).ToArray();            //used to put everything into lower case
            result = arrayThatHasIt;
            return result;
        }
    }
}