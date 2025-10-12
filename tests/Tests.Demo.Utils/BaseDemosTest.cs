using CrossSharp.Application;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace Tests.Demo.Utils;

public abstract class BaseDemosTest<TMainForm> : IDisposable
    where TMainForm : IForm
{
    Thread _appThread;
    protected IApplication _application;

    protected BaseDemosTest()
    {
        _appThread = new Thread(() =>
        {
            var configuration = new BaseConfiguration()
            {
                ApplicationName = "Tests.Demos",
                CompanyName = "CrossSharp",
            };
            var builder = new ApplicationBuilder(configuration);
            builder.Run<TMainForm>();
        });
        // _appThread.SetApartmentState(ApartmentState.STA); // Maybe on windows I need this
        _appThread.Start();

        // Update this with real state of form when implemented, to go while (!form.shown) { Thread.Sleep(100); }
        _appThread.Join(1000);
        _application = Services.GetSingleton<IApplication>();
    }

    public void Dispose()
    {
        _application.MainForm.Close();
    }
}
