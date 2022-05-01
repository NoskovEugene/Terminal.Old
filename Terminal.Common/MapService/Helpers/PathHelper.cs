namespace Terminal.Common.MapService.Helpers;

public static class PathHelper
{
    public static PathBuilder<Type> Source<T>(this PathBuilder<Type> builder)
    {
        return builder.Source(typeof(T));
    }

    public static PathBuilder<Type> Target<T>(this PathBuilder<Type> builder)
    {
        return builder.Target(typeof(T));
    }

    public static bool TryFindPath<T>(this Map<Type> map, out Path<Type> path)
    {
        return map.TryFindPath(typeof(T), out path);
    }

    public static void ConfigurePath<T>(this Map<T> map, Action<PathBuilder<T>> pathBuilderAction)
    {
        var builder = new PathBuilder<T>();
        pathBuilderAction(builder);
        var pair = builder.Build();
        map.AppendPath(pair.Key, pair.Value);
    }
}