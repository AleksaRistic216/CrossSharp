using CrossSharp.Application;
using Demos.GitControl;
using Demos.GitControl.Forms;

var configuration = new BaseConfiguration()
{
    ApplicationName = "Demos.GitControl",
    CompanyName = "CrossSharp",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
