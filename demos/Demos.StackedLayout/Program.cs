using CrossSharp.Application;
using Demos.StackedLayout;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.SimpleForm",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
