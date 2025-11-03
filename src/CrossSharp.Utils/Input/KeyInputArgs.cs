namespace CrossSharp.Utils.Input;

public class KeyInputArgs : System.EventArgs
{
    public KeyCode KeyCode { get; set; }
    public char? Char { get; set; }
}
