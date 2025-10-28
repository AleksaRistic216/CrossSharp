using CrossSharp.Application;
using Demos.TextEditor;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.TextEditor",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
