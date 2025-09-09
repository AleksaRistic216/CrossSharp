using System.Drawing;
namespace CrossSharp.Utils;

public partial class Control {
    public EventHandler<Size>? OnSizeChanged { get; set; }
}