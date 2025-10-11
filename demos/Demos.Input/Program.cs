using CrossSharp.Application;
using Demos.Input;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.Input",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
