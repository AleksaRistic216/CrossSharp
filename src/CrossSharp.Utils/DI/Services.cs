using CrossSharp.Utils.Helpers;

namespace CrossSharp.Utils.DI;

public static class Services
{
    static readonly Dictionary<Type, object> _services = new();

    public static void AddSingleton<T>(T instance, bool overrideExisting = false)
        where T : class
    {
        if (instance is null)
            throw new ArgumentNullException(nameof(instance));
        Type implementationType = typeof(T);
        if (overrideExisting && _services.ContainsKey(implementationType))
        {
            Debug.Log(LogCategory.DI, $"Overriding service: {implementationType.Name} -> {instance.GetType().Name}");
            _services[implementationType] = instance;
            return;
        }
        if (!_services.TryAdd(implementationType, instance))
        {
            Debug.LogError($"Service already registered: {implementationType.FullName}");
            throw new InvalidOperationException(
                $"Service for type {implementationType.FullName} is already registered."
            );
        }
        Debug.Log(LogCategory.DI, $"Registered service: {implementationType.Name} -> {instance.GetType().Name}");
    }

    public static void AddSingleton<TInterface, TImplementation>(bool overrideExisting = false)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        Type interfaceType = typeof(TInterface);
        Type implementationType = typeof(TImplementation);
        if (overrideExisting && _services.ContainsKey(interfaceType))
        {
            Debug.Log(LogCategory.DI, $"Overriding service: {interfaceType.Name} -> {implementationType.Name}");
            _services[interfaceType] = Activator.CreateInstance<TImplementation>();
            return;
        }
        if (_services.ContainsKey(interfaceType))
        {
            Debug.LogError($"Service already registered: {interfaceType.FullName}");
            throw new InvalidOperationException($"Service for type {interfaceType.FullName} is already registered.");
        }
        Debug.Log(LogCategory.DI, $"Registered service: {interfaceType.Name} -> {implementationType.Name}");
        TImplementation implementationInstance = Activator.CreateInstance<TImplementation>();
        _services[interfaceType] = implementationInstance;
    }

    public static TInterface GetSingleton<TInterface>()
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
        Debug.LogError($"Service not registered: {interfaceType.FullName}");
        throw new KeyNotFoundException($"Service for type {interfaceType.FullName} is not registered.");
    }

    public static object GetSingleton(Type interfaceType)
    {
        if (_services.TryGetValue(interfaceType, out object? service))
        {
            return service;
        }
        throw new KeyNotFoundException($"Service for type {interfaceType.FullName} is not registered.");
    }

    public static bool IsRegistered<TInterface>()
        where TInterface : class
    {
        Type interfaceType = typeof(TInterface);
        return _services.ContainsKey(interfaceType);
    }

    public static bool IsRegistered(Type interfaceType) => _services.ContainsKey(interfaceType);
}
