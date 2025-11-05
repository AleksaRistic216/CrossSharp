using CrossSharp.Application;
using Demos.Accordion;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.Accordion",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
