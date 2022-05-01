namespace Terminal.Common.MapService;

public class Path<T>
{
    public T Value { get; set; }
    
    public List<Path<T>> NextPaths { get; set; }
}