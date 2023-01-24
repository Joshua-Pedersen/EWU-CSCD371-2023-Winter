using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests;

[TestClass]
public class LogFactoryTests
{
    [TestMethod]
    public void CreateLogger_NoConfig_Null()
    {
        // Arrange

        LogFactory factory = new();

        // Act

        BaseLogger logger = factory.CreateLogger(nameof(LogFactoryTests));

        // Assert

        Assert.IsNull(logger);
    }

    [TestMethod]
    public void CreateLogger_Config_FileLogger()
    {
        // Arrange

        LogFactory factory = new();
        factory.ConfigureFileLogger("Test");

        // Act

        BaseLogger logger = factory.CreateLogger(nameof(LogFactoryTests));

        // Assert
        Assert.IsInstanceOfType(logger, typeof(FileLogger));
    }
}
