using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Application;

public class BaseConfiguration : IApplicationConfiguration {

    public required string ApplicationName { get; set; }
    public required string CompanyName { get; set; }
}