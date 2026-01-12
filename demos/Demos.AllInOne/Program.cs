using CrossSharp.Application;
using CrossSharp.Icons.Providers;
using CrossSharp.Utils.Enums;
using Demos.AllInOne;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.AllInOne",
    CompanyName = "CrossSharp",
    FormsStyle = FormStyle.CrossSharp,
};
var builder = new ApplicationBuilder(configuration);
builder.SetIconProvider(new CrossSharp2026IconProvider());
builder.Run<MainForm>();
