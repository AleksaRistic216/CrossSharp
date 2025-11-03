namespace CrossSharp.Utils.EventArgs;

public class FilesSelectedEventArgs(string[] selectedFiles) : System.EventArgs
{
    public string[] SelectedFiles { get; private set; } = selectedFiles;
}
