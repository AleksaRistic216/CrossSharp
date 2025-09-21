using CrossSharp.Utils.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface IGtkWidget : IControl, ILocationProvider
{
    void DrawShadows(Graphics g);

    void DrawBackground(Graphics g);

    void DrawBorders(Graphics g);

    void DrawContent(Graphics g);
}
