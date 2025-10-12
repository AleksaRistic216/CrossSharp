namespace Tests.Demo;

static class Constants
{
    internal static IEnumerable<string> MainForms
    {
        get
        {
            return Directory
                .GetFiles(".", "Demos.*.dll", SearchOption.AllDirectories)
                .Select(Path.GetFullPath)
                .ToList();
        }
    }
}
