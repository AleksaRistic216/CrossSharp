using CrossSharp.Application;
using Demos.TabbedLayout;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.TabbedLayout",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.SetTheme(new CustomTheme());
builder.Run<MainForm>();
