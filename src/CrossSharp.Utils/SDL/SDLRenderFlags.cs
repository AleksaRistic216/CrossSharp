namespace CrossSharp.Utils.SDL;

// Note: SDL3 no longer uses renderer flags. This enum is kept for backwards compatibility reference.
// In SDL3, use SDL_SetRenderVSync() and properties-based configuration instead.
[Flags]
enum SDLRenderFlags
{
    /// <summary>
    /// The renderer is a software fallback
    /// </summary>
    SDL_RENDERER_SOFTWARE = 0x00000001,

    /// <summary>
    /// The renderer uses hardware acceleration
    /// </summary>
    SDL_RENDERER_ACCELERATED = 0x00000002,

    /// <summary>
    /// Present is synchronized with the refresh rate
    /// </summary>
    SDL_RENDERER_PRESENTVSYNC = 0x00000004,

    /// <summary>
    /// The renderer supports rendering to texture
    /// </summary>
    SDL_RENDERER_TARGETTEXTURE = 0x00000008,
}
