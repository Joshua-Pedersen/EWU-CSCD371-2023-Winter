using Calculate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculateTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Program_WriteLine_ConsoleOutputEqual()
        {
            // Arrange

            StringWriter captured = new();
            Console.SetOut(captured);
            Program prog = new();
            string testMessage = "testout";

            // Act

            prog.WriteLine(testMessage);

            //Assert

            Assert.AreEqual(testMessage, captured.ToString().Trim());

        }

        [TestMethod]
        public void Program_ReadLine_ConsoleInputEqual()
        {
            // Arrange

            string testMessage = "testin";
            StringReader captured = new(testMessage);
            Console.SetIn(captured);
            Program prog = new();

            // Act

            string? returned = prog.ReadLine();

            // Assert
            Assert.IsNotNull(returned);
            Assert.AreEqual(testMessage, returned);
            
        }
    }
}