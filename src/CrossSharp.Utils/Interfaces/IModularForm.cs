namespace CrossSharp.Utils.Interfaces;

public interface IModularForm
{
    void AddPage(object identifier, Type pageType);
    void NavigateToPage(object identifier);
    string AddPageWithNavigation(string name, Type pageType);
    void RegisterContentSingleton<T>(T instance, bool overrideExisting = false)
        where T : class;
}
