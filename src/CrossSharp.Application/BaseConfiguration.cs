using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

public class BaseConfiguration : IApplicationConfiguration
{
    public bool HighDpi { get; set; }
    public required string ApplicationName { get; set; }
    public required string CompanyName { get; set; }
    public int CoreFps { get; set; } = 120;
}
