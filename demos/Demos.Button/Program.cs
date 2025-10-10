using CrossSharp.Application;
using Demos.Button;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.Label",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
