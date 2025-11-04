using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp;

public class DynamicControlsController(ref IControlsContainer container)
{
    readonly Dictionary<Type, object> _services = new();
    readonly Dictionary<object, Type> _pages = new();
    readonly Dictionary<Type, IControl> _pageInstances = new();
    readonly IControlsContainer _container = container;
    public object? CurrentPage { get; private set; }

    /// <summary>
    /// Registers a control type with an identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="control"></param>
    public void Register(object identifier, Type control)
    {
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

    bool TryGetService(Type type, out object? service)
    {
        // First try getting it from local content
        if (_services.TryGetValue(type, out var value))
        {
            service = value;
            return true;
        }

        // try getting it from global services
        if (Services.IsRegistered(type))
        {
            service = Services.GetSingleton(type);
            return true;
        }
        service = null;
        return false;
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
                        TryGetService(p.ParameterType, out var v)
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

    public object GetCurrentControl()
    {
        if (CurrentPage == null)
            throw new InvalidOperationException("No current page is set.");
        if (!_pages.TryGetValue(CurrentPage, out Type? type))
            throw new ArgumentException("Not found", nameof(CurrentPage));
        if (!_pageInstances.TryGetValue(type, out IControl? value))
            throw new InvalidOperationException("Current page instance not found.");
        return value;
    }
}
