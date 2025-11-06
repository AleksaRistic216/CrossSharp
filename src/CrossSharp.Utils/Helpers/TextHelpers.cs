using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

static class TextHelpers
{
    internal static Size MeasureText(IControl anyControlWithinForm, string text, FontFamily fontFamily, int fontSize)
    {
        var form = anyControlWithinForm.GetForm();
        if (form is not IFormSDL formSdl)
            return Size.Empty;
        using var graphics = new SDLGraphics(formSdl.Renderer);
        return graphics.MeasureText(text, fontFamily, fontSize);
    }
}
