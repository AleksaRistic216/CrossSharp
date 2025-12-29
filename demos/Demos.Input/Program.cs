using CrossSharp.Application;
using CrossSharp.Icons.Providers;
using Demos.Input;

var configuration = new BaseConfiguration() { ApplicationName = "Demos.Input", CompanyName = "CrossSharp" };
var builder = new ApplicationBuilder(configuration);
builder.SetIconProvider(new CrossSharp2026IconProvider());
builder.Run<MainForm>();
