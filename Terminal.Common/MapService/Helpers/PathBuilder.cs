namespace Terminal.Common.MapService.Helpers;

public class PathBuilder<T>
{
    private MutableKeyValuePair<T, List<T>> _pair;

    public PathBuilder()
    {
        _pair = new MutableKeyValuePair<T, List<T>>
        {
            Value = new()
        };
    }

    public PathBuilder<T> Source(T value)
    {
        _pair.Key = value;
        return this;
    }

    public PathBuilder<T> Target(T value)
    {
        _pair.Value.Add(value);
        return this;
    }

    public MutableKeyValuePair<T, List<T>> Build()
    {
        return _pair;
    }
}