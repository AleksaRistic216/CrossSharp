using System.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface ICenterPanelChild
{
    Size GetSize();
    EventHandler? LayoutChanged { get; set; }
}
