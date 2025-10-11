using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class ModularForm : FormBase<IModularFormFactory>, IModularForm
{
    public int ContentHeight => ((IModularForm)Implementation).ContentHeight;

    public void AddPage(object identifier, Type pageType) =>
        ((IModularForm)Implementation).AddPage(identifier, pageType);

    public void NavigateToPage(object identifier) =>
        ((IModularForm)Implementation).NavigateToPage(identifier);

    public string AddPageWithNavigation(string name, Type pageType) =>
        ((IModularForm)Implementation).AddPageWithNavigation(name, pageType);

    public void RegisterContentSingleton<T>(T instance, bool overrideExisting = false)
        where T : class =>
        ((IModularForm)Implementation).RegisterContentSingleton(instance, overrideExisting);
}
