namespace Terminal.Common.MapService;

public class Map<T>
{
    private List<MapItem<T>> _mapItems;

    private List<int> _visited;

    public Map()
    {
        _mapItems = new();
        _visited = new();
    }

    private int NextIndex => _mapItems.Count;

    private int AddItem(T value)
    {
        var item = new MapItem<T>
        {
            Id = NextIndex,
            Value = value,
            ChildrenIndexes = new()
        };
        _mapItems.Add(item);
        return item.Id;
    }

    private MapItem<T> Find(int id)
    {
        if (Exist(id))
        {
            return _mapItems.First(x => x.Id == id);
        }

        throw new Exception("Item not found");
    }

    private bool Exist(int id)
    {
        return _mapItems.Any(x => x.Id == id);
    }

    private bool Exist(T value)
    {
        return _mapItems.Any(x => x.Value.Equals(value));
    }

    public void AddReference(int source, int target)
    {
        var sourceElement = _mapItems.FirstOrDefault(x => x.Id == source);
        if (sourceElement != null && _mapItems.Any(x => x.Id != target))
        {
            sourceElement.ChildrenIndexes.Add(target);
        }
    }

    public void AppendPath(T source, List<T> children)
    {
        if (!Exist(source)) AddItem(source);
        var sourceElement = _mapItems.FirstOrDefault(x => x.Value.Equals(source));
        foreach (var child in children)
        {
            var childIndex = !Exist(child) ? AddItem(child) : _mapItems.First(x => x.Value.Equals(child)).Id;
            sourceElement.ChildrenIndexes.Add(childIndex);
        }
    }

    public bool TryFindPath(T source, out Path<T> path)
    {
        _visited.Clear();
        var rootItem = _mapItems.FirstOrDefault(x => x.Value.Equals(source));
        if (rootItem != null)
        {
            path = CalculateChildren(rootItem);
            return true;
        }

        path = default;
        return false;
    }

    private Path<T> CalculateChildren(MapItem<T> item)
    {
        var result = new Path<T>()
        {
            Value = item.Value,
            NextPaths = new()
        };
        var children = item.ChildrenIndexes;
        _visited.Add(item.Id);
        foreach (var childIndex in children)
        {
            var child = _mapItems.First(x => x.Id == childIndex);
            if (!_visited.Contains(childIndex))
            {
                result.NextPaths.Add(CalculateChildren(child));
                var indx = _visited.IndexOf(child.Id);
                _visited = _visited.Take(indx).ToList();                
            }
            else
            {
                Console.WriteLine("Cycle detected");
            }
        }

        return result;
    }

    public bool ExistPath(T from, T to)
    {
        _visited.Clear();
        var rootItem = _mapItems.FirstOrDefault(x => x.Value.Equals(from));
        if (rootItem != null)
        {
            return ExistPathForItem(rootItem, to);
        }

        return false;
    }

    private bool ExistPathForItem(MapItem<T> item, T rootValue)
    {
        _visited.Add(item.Id);
        foreach (var childrenIndex in item.ChildrenIndexes)
        {
            var child = _mapItems.First(x => x.Id == childrenIndex);
            if (!_visited.Contains(childrenIndex))
            {
                if (child.Value.Equals(rootValue))
                    return true;
                var result = ExistPathForItem(child, rootValue);
                if (result) return true;
                var indx = _visited.IndexOf(child.Id);
                _visited = _visited.Take(indx).ToList();   
            }
            else
            {
                Console.WriteLine("Cycle detected");
            }
        }

        return false;
    }
}