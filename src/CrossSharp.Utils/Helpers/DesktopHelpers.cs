using System.Diagnostics;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Helpers;

static class DesktopHelpers
{
    internal static byte[] TakeScreenshot()
    {
        switch (PlatformHelpers.GetCurrentPlatform())
        {
            case CrossPlatformType.Windows:
                throw new NotImplementedException("Windows screenshot not implemented");
            case CrossPlatformType.Linux:
                return TakeScreenshotLinux();
            case CrossPlatformType.MacOs:
                throw new NotImplementedException("MacOS screenshot not implemented");
            default:
                throw new NotSupportedException("Unsupported platform");
        }
    }

    static byte[] TakeScreenshotLinux()
    {
        string tool;
        string tempFile = Path.Combine(Path.GetTempPath(), $"screenshot_{Guid.NewGuid()}.png");
        string picturesDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        );

        if (File.Exists("/usr/bin/gnome-screenshot"))
        {
            tool = $"gnome-screenshot --file=\"{tempFile}\"";
        }
        else if (File.Exists("/usr/bin/grim"))
        {
            tool = "grim -";
        }
        else if (File.Exists("/usr/bin/import"))
        {
            tool = "import -window root png:-";
        }
        else if (File.Exists("/usr/bin/dbus-send"))
        {
            // Trigger GNOME screenshot portal via D-Bus

            tool =
                "gdbus call --session "
                + "--dest org.freedesktop.portal.Desktop "
                + "--object-path /org/freedesktop/portal/desktop "
                + "--method org.freedesktop.portal.Screenshot.Screenshot "
                + "\":1.0\" \"{}\"";
        }
        else
        {
            throw new Exception("No screenshot tool found");
        }

        var psi = new ProcessStartInfo
        {
            FileName = tool.StartsWith("dbus-send") ? "dbus-send" : "sh",
            Arguments = tool.StartsWith("dbus-send")
                ? tool.Substring("dbus-send ".Length)
                : $"-c \"{tool.Replace("\"", "\\\"")}\"",
            RedirectStandardOutput = tool.Contains("grim") || tool.Contains("import"),
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        using var process = Process.Start(psi);
        if (process == null)
            throw new Exception("Failed to start process");

        using var ms = new MemoryStream();

        if (tool.Contains("grim") || tool.Contains("import"))
        {
            process.StandardOutput.BaseStream.CopyTo(ms);
        }

        string stderr = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (tool.StartsWith("gnome-screenshot"))
        {
            if (!File.Exists(tempFile))
                throw new Exception($"gnome-screenshot failed. stderr: {stderr}");

            ms.Write(File.ReadAllBytes(tempFile));
            File.Delete(tempFile);
        }
        else if (tool.StartsWith("gdbus call"))
        {
            // Wait briefly for screenshot to be saved
            Thread.Sleep(2000);

            var latestFile = Directory
                .GetFiles(picturesDir, "*.png")
                .OrderByDescending(File.GetLastWriteTime)
                .FirstOrDefault();

            if (latestFile == null)
                throw new Exception($"xdg-desktop-portal screenshot failed. stderr: {stderr}");

            ms.Write(File.ReadAllBytes(latestFile));
            File.Delete(latestFile);
        }

        if (ms.Length == 0)
            throw new Exception($"Screenshot failed. stderr: {stderr}");

        return ms.ToArray();
    }
}
