using CrossSharp.Application;
using FormsApp;

var configuration = new BaseConfiguration
{
    ApplicationName = "FormsApp",
    CompanyName = "YourCompany",
};
var builder = new ApplicationBuilder(configuration);
builder.Run<MainForm>();
