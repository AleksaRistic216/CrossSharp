namespace CrossSharp.Utils.Interfaces;

public interface IApplicationConfiguration
{
    bool HighDpi { get; set; }
    string ApplicationName { get; set; }
    string CompanyName { get; set; }
    int CoreFps { get; set; }
}
