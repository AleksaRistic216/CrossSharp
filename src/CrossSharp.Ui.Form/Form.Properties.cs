using System.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Application")]
namespace CrossSharp.Ui;

public partial class Form {
    public IntPtr Handle { get; internal set; }
    public Point Location { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Title { get; set; } = "Form";
    string? _applicationId;
    IApplicationConfiguration _applicationConfiguration;
    internal string ApplicationId => _applicationId ??= ApplicationNameHelpers.FormatApplicationId(_applicationConfiguration.ApplicationName, _applicationConfiguration.CompanyName);
}