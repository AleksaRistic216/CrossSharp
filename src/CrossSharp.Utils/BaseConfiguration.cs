using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

/// <summary>
/// Default implementation of <see cref="IApplicationConfiguration"/> providing common application settings.
/// This class is shared across all platforms (Desktop, Android, etc.).
/// </summary>
public class BaseConfiguration : IApplicationConfiguration
{
    public FormStyle FormsStyle { get; set; } = FormStyle.Native;
    public bool HighDpi { get; set; }
    public required string ApplicationName { get; set; }
    public required string CompanyName { get; set; }
    public int CoreFps { get; set; } = 120;
}
