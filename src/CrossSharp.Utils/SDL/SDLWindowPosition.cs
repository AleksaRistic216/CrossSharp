namespace CrossSharp.Utils.SDL;

static class SDLWindowPosition
{
    internal const int UNDEFINED_MASK = 0x1FFF0000;
    internal const int CENTERED_MASK = 0x2FFF0000;

    internal const int UNDEFINED = UNDEFINED_MASK | 0;
    internal const int CENTERED = CENTERED_MASK | 0;
}
