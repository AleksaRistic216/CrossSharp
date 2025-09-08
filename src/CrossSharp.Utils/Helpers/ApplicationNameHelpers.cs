using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CrossSharp.Ui.Form")]
namespace CrossSharp.Utils.Helpers;

static class ApplicationNameHelpers {
    internal static string FormatApplicationId(string applicationName, string companyName) {
        string formattedAppName = applicationName.Replace(" ", "_").ToLowerInvariant();
        string formattedCompanyName = companyName.Replace(" ", "_").ToLowerInvariant();
        return $"com.{formattedCompanyName}.{formattedAppName}";
    }
}