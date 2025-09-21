namespace CrossSharp.Utils.Interfaces;

public interface IApplicationLoop : IDisposable
{
    void Run<T>()
        where T : IForm;
}
