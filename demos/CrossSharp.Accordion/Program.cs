using CrossSharp.Accordion;
using CrossSharp.Application;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.Accordion",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
