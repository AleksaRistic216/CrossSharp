using CrossSharp.Utils.DI;
using Xunit;

namespace Tests.Utils;

// ReSharper disable once InconsistentNaming
public class DITests
{
    class AddSingletonTestService;

    [Fact]
    public void AddSingletonTest()
    {
        Services.AddSingleton(new AddSingletonTestService());
    }

    [Fact]
    public void AddSingletonNullTest()
    {
        AddSingletonTestService service = null!;
        Assert.Throws<ArgumentNullException>(() =>
        {
            Services.AddSingleton(service);
        });
    }

    class GetSingletonTestService
    {
        internal int Value;
    }

    [Fact]
    public void GetSingletonTest()
    {
        var value = Random.Shared.Next();
        Services.AddSingleton(new GetSingletonTestService { Value = value });
        var service = Services.GetSingleton<GetSingletonTestService>();
        Assert.NotNull(service);
        Assert.Equal(value, service.Value);
    }

    class IsRegisteredTrueTestService;

    [Fact]
    public void IsRegisteredTrueTest()
    {
        Services.AddSingleton(new IsRegisteredTrueTestService());
        Assert.True(Services.IsRegistered<IsRegisteredTrueTestService>());
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    class IsRegisteredFalseTestService;

    [Fact]
    public void IsRegisteredFalseTest()
    {
        Assert.False(Services.IsRegistered<IsRegisteredFalseTestService>());
    }
}
