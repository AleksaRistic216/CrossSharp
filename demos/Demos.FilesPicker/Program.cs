using CrossSharp.Application;
using Demos.FilesPicker;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.FilesPicker",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
