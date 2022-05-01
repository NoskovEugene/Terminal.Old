using Terminal.Common.MapService.Helpers;

namespace Terminal.Common.MapService;

public class MapProfile<T>
{
    private List<MutableKeyValuePair<T, List<T>>> _pairs;

    public void CreatePath(Action<PathBuilder<T>> builderAction)
    {
        var builder = new PathBuilder<T>();
        builderAction(builder);
        _pairs.Add(builder.Build());
    }

    public List<MutableKeyValuePair<T, List<T>>> GetPairs() => _pairs;
}