using CrossSharp.Ui;
namespace CrossSharp.Application;

class Loop {
    readonly CancellationTokenSource _cts = new ();
    Form _mainForm;
    public void Run<T>() where T : Form {
        _ = Input.StartAsync(_cts.Token);
        _mainForm = Activator.CreateInstance<T>();
        _mainForm.Show();
        while (!_cts.Token.IsCancellationRequested) {
            Thread.Sleep(100);
            Console.WriteLine("Running application loop...");
        }
        Console.WriteLine("Application loop has been cancelled.");
    }
}