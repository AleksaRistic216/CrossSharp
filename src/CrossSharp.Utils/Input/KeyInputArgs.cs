namespace CrossSharp.Utils.Input;

public class KeyInputArgs : System.EventArgs
{
    public KeyCode KeyCode { get; set; }
    public char? Char { get; set; }
    public bool IsShiftPressed { get; set; }
    public bool IsCtrlPressed { get; set; }
    public bool IsAltPressed { get; set; }
}
