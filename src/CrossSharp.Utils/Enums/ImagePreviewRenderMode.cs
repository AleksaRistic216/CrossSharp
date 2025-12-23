namespace CrossSharp.Utils.Enums;

public enum ImagePreviewRenderMode
{
    /// <summary>
    /// Keep original image in memory but render to smaller texture for performance.
    /// This is a balanced approach between memory usage and quality.
    /// </summary>
    Default,

    /// <summary>
    /// Pre-scale source image to save memory.
    /// Best for displaying many images or very large images.
    /// </summary>
    Performance,

    /// <summary>
    /// Full resolution rendering without any scaling.
    /// Best quality but highest memory usage.
    /// </summary>
    Quality,
}
