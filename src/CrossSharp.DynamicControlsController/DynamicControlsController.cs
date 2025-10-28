using CrossSharp.Utils.Interfaces;

namespace CrossSharp;

public class DynamicControlsController(ref IControlsContainer container)
{
    readonly Dictionary<Type, object> _services = new();
    readonly Dictionary<object, Type> _pages = new();
    readonly Dictionary<Type, IControl> _pageInstances = new();
    readonly IControlsContainer _container = container;
    public object? CurrentPage { get; private set; }

    public void Set(object identifier, Type control)
    {
        if (!typeof(IControl).IsAssignableFrom(control))
            throw new ArgumentException(
                "Control must implement IControl interface",
                nameof(control)
            );
        _pages[identifier] = control;
    }

    public void AddSingleton<T>(T instance, bool overrideExisting = false)
        where T : class
    {
        Type implementationType = typeof(T);
        if (overrideExisting && _services.ContainsKey(implementationType))
        {
            _services[implementationType] = instance;
            return;
        }
        if (!_services.TryAdd(implementationType, instance))
            throw new InvalidOperationException(
                $"Service for type {implementationType.FullName} is already registered."
            );
    }

    public void Show(object identifier)
    {
        if (Equals(CurrentPage, identifier))
            return;
        if (!_pages.TryGetValue(identifier, out Type? type))
            throw new ArgumentException("Not found", nameof(identifier));
        if (!_pageInstances.TryGetValue(type, out IControl? value))
        {
            var ctor = type.GetConstructors().FirstOrDefault();
            var parameters = ctor?.GetParameters();
            IControl? instance;
            if (parameters != null && parameters.Length > 0)
            {
                // You need to provide appropriate parameter values here
                // For example, using default values or from a factory/service
                var args = parameters
                    .Select(p =>
                        _services.TryGetValue(p.ParameterType, out var v)
                            ? v
                            : throw new Exception(
                                "No registered service for " + p.ParameterType.FullName
                            )
                    )
                    .ToArray();
                instance = (IControl?)Activator.CreateInstance(type, args);
            }
            else
            {
                instance = (IControl?)Activator.CreateInstance(type);
            }
            value =
                instance ?? throw new Exception("Failed to create instance of " + type.FullName);
            _pageInstances[type] = value;
        }
        _container.Remove(_container.ToArray());
        _container.Add(value);
        value.Invalidate();
        CurrentPage = identifier;
    }
}
