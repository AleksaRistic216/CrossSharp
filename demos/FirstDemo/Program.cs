using CrossSharp.Application;
using FirstDemo;

Builder builder = new(
    new BaseConfiguration() { ApplicationName = "MyApp", CompanyName = "MyCompany" }
);

builder.EnableDevelopersMode();
builder.SetTheme(new CustomTheme());
builder.Run<MainForm>();
