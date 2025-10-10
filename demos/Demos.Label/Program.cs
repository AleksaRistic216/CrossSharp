using CrossSharp.Application;
using Demos.Label;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.Label",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
