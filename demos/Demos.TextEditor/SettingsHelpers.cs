using Demos.TextEditor.Models;

namespace Demos.TextEditor;

static class SettingsHelpers
{
    static Settings _settings;

    static SettingsHelpers()
    {
        var applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var applicationSettingsFile = Path.Combine(
            applicationDataDirectory,
            "CrossSharp",
            "Demos",
            "TextEditor",
            "settings.json"
        );
        if (!File.Exists(applicationSettingsFile))
        {
            _settings = new Settings();
            return;
        }
        var json = File.ReadAllText(applicationSettingsFile);
        _settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
    }

    public static Settings GetSettings()
    {
        return _settings;
    }

    public static void SaveSettings(Settings settings)
    {
        _settings = settings;
        var applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var applicationSettingsFile = Path.Combine(
            applicationDataDirectory,
            "CrossSharp",
            "Demos",
            "TextEditor",
            "settings.json"
        );
        var applicationSettingsDirectory = Path.GetDirectoryName(applicationSettingsFile);
        if (!string.IsNullOrWhiteSpace(applicationSettingsDirectory) && !Directory.Exists(applicationSettingsDirectory))
            Directory.CreateDirectory(applicationSettingsDirectory);
        var json = System.Text.Json.JsonSerializer.Serialize(settings);
        File.WriteAllText(applicationSettingsFile, json);
    }
}
