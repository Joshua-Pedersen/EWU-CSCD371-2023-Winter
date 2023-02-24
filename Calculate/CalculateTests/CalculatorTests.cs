using Calculate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculateTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Calculator_TryCalculateIncorrectFormat_ReturnsFalse()
        {
            // Arrange

            Calculator calc = new();
            Program prog = new();
            double result = default;

            // Act

            var stringReader = new StringReader("+ 4");
            Console.SetIn(stringReader);

            // Assert
            Assert.IsFalse(calc.TryCalculate(prog.ReadLine()!, out result));
        }

        [TestMethod]
        public void Calculator_Addition_True()
        {
            // Arrange

            Calculator calc = new();
            Program prog = new();
            var stringReader = new StringReader("42 + 1");
            Console.SetIn(stringReader);
            double result = default;

            // Act

            calc.TryCalculate(prog.ReadLine()!, out result);

            // Assert
            Assert.AreEqual(43, result);
        }

        [TestMethod]
        public void Calculator_Subtraction_True()
        {
            // Arrange

            Calculator calc = new();
            Program prog = new();
            var stringReader = new StringReader("42 - 1");
            Console.SetIn(stringReader);
            double result = default;

            // Act

            calc.TryCalculate(prog.ReadLine()!, out result);

            // Assert
            Assert.AreEqual(41, result);
        }

        [TestMethod]
        public void Calculator_Multiplication_True()
        {
            // Arrange

            Calculator calc = new();
            Program prog = new();
            var stringReader = new StringReader("42 * 2");
            Console.SetIn(stringReader);
            double result = default;

            // Act

            calc.TryCalculate(prog.ReadLine()!, out result);

            // Assert
            Assert.AreEqual(84, result);
        }

        [TestMethod]
        public void Calculator_Division_True()
        {
            // Arrange

            Calculator calc = new();
            Program prog = new();
            var stringReader = new StringReader("42 / 2");
            Console.SetIn(stringReader);
            double result = default;

            // Act

            calc.TryCalculate(prog.ReadLine()!, out result);

            // Assert
            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void Calculator_TryCalculateNoWhiteSpace_Invalid()
        {
            // Arrange

            Calculator calc = new();
            Program prog = new();
            double result = default;

            // Act

            var stringReader = new StringReader("5*1");
            Console.SetIn(stringReader);

            // Assert
            Assert.IsFalse(calc.TryCalculate(prog.ReadLine()!, out result));
        }
    }
}