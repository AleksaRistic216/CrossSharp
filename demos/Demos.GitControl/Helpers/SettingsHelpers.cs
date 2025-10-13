using System.Text.Json;
using Demos.GitControl.Enums;

namespace Demos.GitControl.Helpers;

static class SettingsHelpers
{
    const string SettingsFileName = "settings.json";

    public static string GetSettingsFilePath(SettingKey key)
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appData, "CrossSharp", "Demos", "GitControl");
        Directory.CreateDirectory(appFolder);
        return Path.Combine(appFolder, key + SettingsFileName);
    }

    public static T LoadSettings<T>(SettingKey key)
        where T : new()
    {
        var path = GetSettingsFilePath(key);
        if (!File.Exists(path))
            return new T();
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json) ?? new T();
    }
}
