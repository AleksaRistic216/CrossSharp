using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IApplicationConfiguration
{
    /// <summary>
    /// Indicates which style of forms to use.
    /// This affects title bars, borders, and other window decorations.
    /// Native style uses the operating system's default window decorations.
    /// CrossSharp style uses custom decorations provided by the CrossSharp framework.
    /// </summary>
    FormStyle FormsStyle { get; set; }
    bool HighDpi { get; set; }
    string ApplicationName { get; set; }
    string CompanyName { get; set; }
    int CoreFps { get; set; }
}
