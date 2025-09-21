namespace CrossSharp.Utils.DI;

static class ServicesPool
{
    static readonly Dictionary<Type, object> _services = new();

    internal static void AddSingleton<T>(T instance, bool overrideExisting = false)
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

    internal static void AddSingleton<TInterface, TImplementation>(bool overrideExisting = false)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        Type interfaceType = typeof(TInterface);
        if (overrideExisting && _services.ContainsKey(interfaceType))
        {
            _services[interfaceType] = Activator.CreateInstance<TImplementation>();
            return;
        }
        if (_services.ContainsKey(interfaceType))
        {
            throw new InvalidOperationException(
                $"Service for type {interfaceType.FullName} is already registered."
            );
        }
        TImplementation implementationInstance = Activator.CreateInstance<TImplementation>();
        _services[interfaceType] = implementationInstance;
    }

    internal static TInterface GetSingleton<TInterface>()
        where TInterface : class
    {
        Type interfaceType = typeof(TInterface);
        if (_services.TryGetValue(interfaceType, out object? service))
        {
            return service as TInterface
                ?? throw new InvalidCastException(
                    $"Registered service cannot be cast to type {interfaceType.FullName}."
                );
        }
        throw new KeyNotFoundException(
            $"Service for type {interfaceType.FullName} is not registered."
        );
    }

    internal static bool IsRegistered<TInterface>()
        where TInterface : class
    {
        Type interfaceType = typeof(TInterface);
        return _services.ContainsKey(interfaceType);
    }

    internal static List<object> GetAllSingletons()
    {
        return _services.Values.ToList();
    }
}
