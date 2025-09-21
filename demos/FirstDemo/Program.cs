using CrossSharp.Application;
using CrossSharp.Utils.DI;
using FirstDemo;

Builder builder = new(
    new BaseConfiguration() { ApplicationName = "MyApp", CompanyName = "MyCompany" }
);

// builder.EnableDevelopersMode();
builder.Run<MainForm>();
