using System.Text.RegularExpressions;
using System.Xml.Linq;
using Xunit;

namespace Tests.Utils;

public class NativeLibraryNamingTests
{
    private static readonly string SolutionRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..")
    );

    /// <summary>
    /// Extracts native library filenames from SDLHelpers.cs constants.
    /// </summary>
    private static HashSet<string> GetExpectedFilenamesFromSDLHelpers()
    {
        var sdlHelpersPath = Path.Combine(
            SolutionRoot,
            "src",
            "CrossSharp.Utils",
            "SDL",
            "SDLHelpers.cs"
        );
        var content = File.ReadAllText(sdlHelpersPath);

        // Match patterns like: internal const string LIB = "runtimes/...";
        var regex = new Regex(
            @"internal\s+const\s+string\s+\w+\s*=\s*""([^""]+)""",
            RegexOptions.Compiled
        );

        var filenames = new HashSet<string>();
        foreach (Match match in regex.Matches(content))
        {
            var path = match.Groups[1].Value;
            var filename = Path.GetFileName(path);
            filenames.Add(filename);
        }

        return filenames;
    }

    [Fact]
    public void NativeLibraryFilenames_ShouldMatchSDLHelperConstants()
    {
        var expectedFilenames = GetExpectedFilenamesFromSDLHelpers();
        Assert.NotEmpty(expectedFilenames);

        var srcPath = Path.Combine(SolutionRoot, "src");
        var csprojFiles = Directory.GetFiles(srcPath, "*.csproj", SearchOption.AllDirectories);

        var violations = new List<string>();

        foreach (var csprojFile in csprojFiles)
        {
            var doc = XDocument.Load(csprojFile);
            var noneElements = doc.Descendants("None")
                .Where(e =>
                {
                    var include = e.Attribute("Include")?.Value;
                    return include != null && include.Contains("lib/sdl3");
                });

            foreach (var element in noneElements)
            {
                var includePath = element.Attribute("Include")?.Value;
                var relativeCsproj = Path.GetRelativePath(SolutionRoot, csprojFile);

                if (includePath != null)
                {
                    var includeFilename = Path.GetFileName(includePath);
                    if (!expectedFilenames.Contains(includeFilename))
                    {
                        violations.Add(
                            $"{relativeCsproj}: Include filename '{includeFilename}' not found in SDLHelpers constants. " +
                            $"Expected one of: {string.Join(", ", expectedFilenames)}"
                        );
                    }
                }
            }
        }

        Assert.True(
            violations.Count == 0,
            $"Native library filenames in csproj Include paths must match SDLHelpers.cs constants.\n" +
            $"Violations:\n{string.Join("\n", violations)}"
        );
    }

    [Fact]
    public void NativeLibraryFiles_ShouldExist()
    {
        var srcPath = Path.Combine(SolutionRoot, "src");
        var csprojFiles = Directory.GetFiles(srcPath, "*.csproj", SearchOption.AllDirectories);

        var missingFiles = new List<string>();

        foreach (var csprojFile in csprojFiles)
        {
            var csprojDir = Path.GetDirectoryName(csprojFile)!;
            var doc = XDocument.Load(csprojFile);
            var noneElements = doc.Descendants("None")
                .Where(e =>
                {
                    var include = e.Attribute("Include")?.Value;
                    return include != null && include.Contains("lib/sdl3");
                });

            foreach (var element in noneElements)
            {
                var includePath = element.Attribute("Include")?.Value ?? "";
                var fullPath = Path.GetFullPath(Path.Combine(csprojDir, includePath));

                if (!File.Exists(fullPath))
                {
                    var relativeCsproj = Path.GetRelativePath(SolutionRoot, csprojFile);
                    missingFiles.Add($"{relativeCsproj}: {includePath}");
                }
            }
        }

        Assert.True(
            missingFiles.Count == 0,
            $"Native library files referenced in csproj files do not exist:\n" +
            string.Join("\n", missingFiles)
        );
    }
}
