using CrossSharp.Application;
using Demos.FlowLayout;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.FlowLayout",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
