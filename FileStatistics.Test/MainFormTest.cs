using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp2;

namespace FileStatistics.Test
{
    public class MainFormTest
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
    }
}
