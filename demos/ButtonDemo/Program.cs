using ButtonDemo;
using CrossSharp.Application;

Builder builder = new(
    new BaseConfiguration() { ApplicationName = "MyApp", CompanyName = "MyCompany" }
);

builder.Run<MainForm>();
