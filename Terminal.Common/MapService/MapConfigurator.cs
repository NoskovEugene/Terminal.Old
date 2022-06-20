namespace Terminal.Common.MapService;

public class MapConfigurator<T>
{
    private Map<T> _map;

    public MapConfigurator()
    {
        _map = new();
    }

    public MapConfigurator<T> UseProfile(MapProfile<T> profile)
    {
        var pairs = profile.GetPairs();
        pairs.ForEach(x =>
        {
            _map.AppendPath(x.Key, x.Value);
        });
        return this;
    }

    public MapConfigurator<T> UseProfile<TProfile>()
    where TProfile: MapProfile<T>
    {
        var instance = Activator.CreateInstance<TProfile>();
        return UseProfile(instance);
    }

    public Map<T> Build() => _map;
}