using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

class Application : IApplication
{
    bool _developersMode;
    IForm? _mainForm;
    public Type? MainFormType { get; set; }
    public IntPtr MainWindowHandle { get; set; }
    public bool DevelopersMode
    {
        get => _developersMode;
        set
        {
            if (_developersMode == value)
                return;
            _developersMode = value;
            RaiseDevelopersModeChanged();
        }
    }
    public EventHandler? DevelopersModeChanged { get; set; }

    public void SetTheme(ITheme theme)
    {
        ServicesPool.AddSingleton(theme, true);
    }

    public void Start()
    {
        if (MainFormType is null)
            throw new NullReferenceException(nameof(MainFormType));
        var services = ServicesPool.GetAllSingletons();
        var typeToCreate = MainFormType;
        var constructor = typeToCreate.GetConstructors().First();
        var parameters = constructor.GetParameters();
        var parameterInstances = new object?[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            var paramType = parameters[i].ParameterType;
            parameterInstances[i] = services.FirstOrDefault(s =>
                paramType.IsAssignableFrom(s.GetType())
            );
            if (parameterInstances[i] is null)
                throw new Exception(
                    $"Cannot resolve dependency of type {paramType} for {typeToCreate.FullName}"
                );
        }
        _mainForm = (IForm)Activator.CreateInstance(MainFormType, parameterInstances)!;
        _mainForm.OnClose += OnMainFormClose;
        _mainForm.Show();
    }

    void OnMainFormClose(object? sender, EventArgs e)
    {
        ServicesPool.GetSingleton<IApplicationLoop>().Dispose();
    }

    void RaiseDevelopersModeChanged()
    {
        DevelopersModeChanged?.Invoke(this, EventArgs.Empty);
    }
}
