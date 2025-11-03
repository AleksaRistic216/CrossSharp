using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Extensions;

public static class IMarginProviderExtensions
{
    public static void SetMargin(this IMarginProvider marginProvider, int all)
    {
        marginProvider.MarginTop = all;
        marginProvider.MarginBottom = all;
        marginProvider.MarginLeft = all;
        marginProvider.MarginRight = all;
        marginProvider.MarginChanged?.Invoke(marginProvider, System.EventArgs.Empty);
    }

    public static void SetMargin(this IMarginProvider marginProvider, int horizontal, int vertical)
    {
        marginProvider.MarginLeft = horizontal;
        marginProvider.MarginRight = horizontal;
        marginProvider.MarginTop = vertical;
        marginProvider.MarginBottom = vertical;
        marginProvider.MarginChanged?.Invoke(marginProvider, System.EventArgs.Empty);
    }

    public static void SetMargin(
        this IMarginProvider marginProvider,
        int left,
        int top,
        int right,
        int bottom
    )
    {
        marginProvider.MarginLeft = left;
        marginProvider.MarginTop = top;
        marginProvider.MarginRight = right;
        marginProvider.MarginBottom = bottom;
        marginProvider.MarginChanged?.Invoke(marginProvider, System.EventArgs.Empty);
    }
}
