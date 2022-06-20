namespace Terminal.Common.MapService;

public class MapItem<T>
{
    public int Id { get; set; }
    
    public T Value { get; set; }
    
    public List<int> ChildrenIndexes { get; set; }
}