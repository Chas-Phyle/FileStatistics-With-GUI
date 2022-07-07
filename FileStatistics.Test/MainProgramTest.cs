using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStatistics.Test
{
    public class MainProgramTest
    {
        [Test]
        public void Remove_ShouldRemoveAllSpecialCharacters ()
        {
            // Arrange
            string[] expected = { "a", "b", "c"};

            string[] testPayload = {"A","b","C", ",",".","-", "—", @"""", "\"",";","!","`","?", "`", "“", "”", "‘", ":", @"\r"
            ,@"\n",@"\t"};
            //Act

            string[] actual = Program.FileCleaner(testPayload);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test] //23 A's, 22 B's, 21 C's, ... 4 T's
        public void Return_ShouldReturnAnOrganizedArray()
        {   //Arrange
            string[,] expected = { {"t","4"},{"s","5" } ,{"r","6" },{"q","7" },{"p","8" },{"o","9" },{"n","10" },{"m","11" },{"l","12" },{"k","13" },{"j","14" },{"i","15" },{"h","16" },{"g","17" },{"f","18"},{"e","19"},{"d","20"},{ "c","21"},{ "b", "22" },{ "a","23" } };
            

            string testPayload = @"C:\Users\chas\source\repos\WinFormsApp2\FileStatistics.Test\testDocument.txt";
        
            //Act
            string[,] actual = Program.fileStatistics(testPayload);

            //Assert
           
            Assert.AreEqual(expected, actual);
        }
    }
}
