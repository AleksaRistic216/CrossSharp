namespace CrossSharp.Utils;

public abstract class CrossWrapper<T>(T implementation)
    where T : class
{
    protected T GetImplementation() => implementation;

    protected bool Equals(CrossWrapper<T> other)
    {
        return EqualityComparer<T>.Default.Equals(implementation, other.GetImplementation());
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(implementation);
    }

    public override bool Equals(object? obj)
    {
        if (obj is CrossWrapper<T> other)
            return Equals(implementation, other.GetImplementation());
        return Equals(implementation, obj);
    }

    public static bool operator ==(CrossWrapper<T>? left, object? right)
    {
        if (left is null)
            return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(CrossWrapper<T>? left, object? right)
    {
        return !(left == right);
    }
}
