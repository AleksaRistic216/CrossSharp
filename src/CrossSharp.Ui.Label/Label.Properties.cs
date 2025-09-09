using System.Drawing;
namespace CrossSharp.Ui;

public partial class Label {
    public Point Location { get; set; }
    public float FontSize { get; set; }
    public IntPtr ContainerHandle { get; set; }
    public IntPtr Handle { get; private set; }
    public IntPtr ParentHandle { get; set; }
}