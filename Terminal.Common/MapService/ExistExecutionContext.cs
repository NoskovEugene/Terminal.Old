namespace Terminal.Common.MapService;

public class ExistExecutionContext<T>
{
    public MapItem<T> Item { get; set; }
    
    public T ToValue { get; set; }

    public bool Exist { get; set; } = false;
}