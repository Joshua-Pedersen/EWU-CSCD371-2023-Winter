using IntelliTect.TestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Tests;

[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", "localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int exitCode = Sut.Run("google.com").ExitCode;
        Assert.AreEqual<int>(0, exitCode);
    }


    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput) = Sut.Run("badaddress");
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.AreEqual<string?>(
            "Ping request could not find host badaddress. Please check the name and try again.".Trim(),
            stdOutput,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunTaskAsync_Success()
    {
        // Arrange

        Task<PingResult> returned;

        // Act

        returned = Sut.RunTaskAsync("localhost");

        // Assert

        AssertValidPingOutput(returned.Result);
    }

    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        // Arrange

        Task<PingResult> returned;

        // Act

        returned = Sut.RunAsync("localhost");

        // Assert

        AssertValidPingOutput(returned.Result);
    }

    [TestMethod]
    async public Task RunAsync_UsingTpl_Success()
    {
        // Arrange

        PingResult returned = await

        // Act

        Sut.RunAsync("localhost");

        // Assert

        AssertValidPingOutput(returned);
    }


    [TestMethod]
    [ExpectedException(typeof(AggregateException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping()
    {
        // Arrange

        CancellationTokenSource cts = new CancellationTokenSource();

        // Act

        Task<PingResult> task = Sut.RunAsync("localhost", cts.Token);
        cts.Cancel();
        task.Wait();

        // Assert
    }

    [TestMethod]
    [ExpectedException(typeof(TaskCanceledException))]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException()
    {
        // Arrage

        // Act

        try
        {
            RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping();
        } catch (AggregateException ex)
        {
            throw ex.Flatten().InnerException!;
        }

        // Assert
    }

    [TestMethod]
    async public Task RunAsync_MultipleHostAddresses_True()
    {
        // Arrange

        string[] hostNames = new string[] { "localhost", "localhost", "localhost", "localhost" };
        int? expectedLineCount = PingOutputLikeExpression.Split(Environment.NewLine).Length + hostNames.Length - 1;
        
        // Act

        PingResult results = await Sut.RunAsync(hostNames);
        int? lineCount = results.StdOutput?.Split(Environment.NewLine).Length;
        
        // Assert

        Assert.AreEqual(expectedLineCount, lineCount);
    }

    [TestMethod]
    async public Task RunLongRunningAsync_UsingTpl_Success()
    {
        // Arrange

        PingResult result = default;

        // Act

        result = await
        Sut.RunLongRunningAsync("localhost");
        
        // Assert

        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
        System.Text.StringBuilder stringBuilder = new();
        numbers.AsParallel().ForAll(item => stringBuilder.AppendLine(""));
        int lineCount = stringBuilder.ToString().Split(Environment.NewLine).Length;
        Assert.AreNotEqual(lineCount, numbers.Count()+1);
    }

    readonly string PingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *, Maximum = *, Average = *".Trim();
    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.IsTrue(stdOutput?.IsLike(PingOutputLikeExpression)??false,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(0, exitCode);
    }
    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunTaskAsync_NullParam_Exception()
    {
        // Arrange

        // Act

        Sut.RunTaskAsync("");

        // Assert
    }
}
