using CrossSharp.Utils.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface IClipLimiter
{
    void LimitClip(ref Graphics g);
}
