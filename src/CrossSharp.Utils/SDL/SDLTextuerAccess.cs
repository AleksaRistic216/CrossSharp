namespace CrossSharp.Utils.SDL;

public enum SDLTextureAccess
{
    Static, // Changes rarely, not lockable
    Streaming, // Changes frequently, lockable
    Target, // Texture can be used as a render target
}
