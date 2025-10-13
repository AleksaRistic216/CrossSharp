using CrossSharp.Application;
using Demos.GitControl;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.GitControl",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
