using CrossSharp.Application;
using CrossSharp.Icons.Providers;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using Demos.TextEditor;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.TextEditor",
    CompanyName = "CrossSharp",
    FormsStyle = FormStyle.CrossSharp,
};
var builder = new ApplicationBuilder(configuration);
builder.SetTheme(new CustomTheme());
builder.SetIconProvider(new CrossSharp2026IconProvider());
builder.Run<MainForm>();

return;
