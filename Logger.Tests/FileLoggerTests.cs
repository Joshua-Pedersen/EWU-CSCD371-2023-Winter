using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Logger.Tests;

[TestClass]
public class FileLoggerTests
{
    [TestMethod]
    public void Log_Message_AppendsInfo()
    {
        // Arrange

        FileLogger logger = new FileLogger(@"../Path.txt") {ClassName = nameof(FileLoggerTests)};


        // Act
        logger.Log(LogLevel.Information, "Hello there");
        DateTime sync = DateTime.Now;
        string output = File.ReadAllText(@"../Path.txt");
        File.Delete(@"../Path.txt");
        //Assert
        // Can't get datetime to sync up correctly

        Assert.IsTrue(output.Contains($"{sync} {nameof(FileLoggerTests)} Informaton: Hello there"));
        
    }
}
