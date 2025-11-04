namespace Demos.TextEditor.Models;

public class Settings
{
    /// <summary>
    /// Files currently opened in the text editor.
    /// Set of file paths.
    /// </summary>
    public HashSet<string> Files { get; set; } = new();
}
