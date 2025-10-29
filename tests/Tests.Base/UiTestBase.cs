using CrossSharp.Application;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace Tests.Base;

public abstract class UiTestBase : IDisposable
{
    Thread _formThread;
    public UiTestForm Form => (UiTestForm)Services.GetSingleton<IApplication>().MainForm;

    protected UiTestBase()
    {
        _formThread = new Thread(() =>
        {
            var configuration = new BaseConfiguration
            {
                ApplicationName = "UiTest",
                CompanyName = "CrossSharp",
            };
            var builder = new ApplicationBuilder(configuration);
            builder.Run<UiTestForm>();
        });
        // thread.SetApartmentState(ApartmentState.STA); // need for windows
        _formThread.Start();
        // Wait for application to be registered
        while (!Services.IsRegistered<IApplication>())
            Thread.Sleep(100);

        // Wait for application to be fully started
        var maxTries = 50;
        var currentTry = 0;
        while (true)
        {
            try
            {
                // Accessing MainForm will throw if not started
                var mf = Services.GetSingleton<IApplication>().MainForm;
                break;
            }
            catch (InvalidOperationException)
            {
                currentTry++;
                if (currentTry >= maxTries)
                    throw new TimeoutException("Timed out waiting for application to start");
                // Keep looping if exception is thrown
            }
            Thread.Sleep(100);
        }
        _formThread.Join(500);
    }

    public void Dispose()
    {
        if (!_formThread.IsAlive)
            return;
        Services.GetSingleton<IApplication>().MainForm.Close();
        _formThread.Join(500);
    }
}
