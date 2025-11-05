using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Application;

public class BaseConfiguration : IApplicationConfiguration
{
    public FormStyle FormsStyle { get; set; } = FormStyle.CrossSharp;
    public bool HighDpi { get; set; }
    public required string ApplicationName { get; set; }
    public required string CompanyName { get; set; }
    public int CoreFps { get; set; } = 120;
}
