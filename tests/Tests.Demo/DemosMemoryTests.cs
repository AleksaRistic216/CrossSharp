using System.Diagnostics;
using Xunit;

namespace Tests.Demo;

public class DemosMemoryTests
{
    private static readonly int DefaultMemoryLimitMb = 150;
    private static readonly Dictionary<string, int> MemoryLimitMb = new()
    {
        { "Demos.Button.dll", 200 }
    };

    public static IEnumerable<object[]> MainFormTypes =>
        Constants.MainForms.Select(type => new object[] { type });

    [Theory]
    [MemberData(nameof(MainFormTypes), MemberType = typeof(DemosStartTests))]
    public void DemosMemoryLimitStartupTest(string demoDllPath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"\"{demoDllPath}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
        };

        using var process = Process.Start(startInfo);
        try
        {
            Thread.Sleep(Utils.Constants.InitialApplicationStartupTimeout);
            Assert.False(process!.HasExited, $"Process for {demoDllPath} exited prematurely");
            var processMemory = process.WorkingSet64;
            var workingSetMb = processMemory / 1024.0 / 1024.0;
            var dllName = System.IO.Path.GetFileName(demoDllPath);
            var limitMb = MemoryLimitMb.GetValueOrDefault(dllName, DefaultMemoryLimitMb);
            Assert.True(
                workingSetMb < limitMb,
                $"Process for {demoDllPath} is using too much memory: {workingSetMb:F2} MB"
            );
        }
        finally
        {
            process?.Kill(); // cleanup
            process?.WaitForExit();
        }
    }
}
