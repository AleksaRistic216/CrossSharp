namespace Demos.TextEditor;

public class TextEditTabDataProvider
{
    HashSet<string> _files = [];
    Dictionary<string, string> _fileContents = new();
    Dictionary<string, string> _tabFileMap = new();

    public bool IsFileOpen(string filePath) => _files.Contains(filePath);

    public void LoadFile(string file, string tabTitle)
    {
        _files.Add(file);
        _fileContents[file] = File.ReadAllText(file);
        _tabFileMap[tabTitle] = file;
    }

    public void CloseFile(string file)
    {
        _files.Remove(file);
        _fileContents.Remove(file);
    }

    public string GetFileContents(string file)
    {
        if (!_tabFileMap.TryGetValue(file, out var filePath))
            throw new ArgumentException("File not found", nameof(file));
        return _fileContents[filePath];
    }

    public HashSet<string> GetOpenFiles() => _files;

    public void SaveFileContents(string title, string inputText)
    {
        if (!_tabFileMap.TryGetValue(title, out var filePath))
            throw new ArgumentException("File not found", nameof(title));
        File.WriteAllText(filePath, inputText);
        _fileContents[filePath] = inputText;
    }
}
