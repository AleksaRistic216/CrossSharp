using System.Diagnostics;
using Xunit;

namespace Tests.Demo;

public class DemosStartTests
{
    static IEnumerable<string> MainForms
    {
        get
        {
            return Directory
                .GetFiles(".", "Demos.*.dll", SearchOption.AllDirectories)
                .Select(Path.GetFullPath)
                .ToList();
        }
    }

    public static IEnumerable<object[]> MainFormTypes =>
        MainForms.Select(type => new object[] { type });

    [Theory]
    [MemberData(nameof(MainFormTypes), MemberType = typeof(DemosStartTests))]
    public void DemoStartWithoutCrashTest(string demoDllPath)
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
        Thread.Sleep(3000);
        Assert.False(process!.HasExited, $"Process for {demoDllPath} exited prematurely");
        process.Kill(); // cleanup
    }
}
