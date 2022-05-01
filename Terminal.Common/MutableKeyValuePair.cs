namespace Terminal.Common;

public class MutableKeyValuePair<TKey, TValue>
{
    public TKey Key { get; set; }

    public TValue Value { get; set; }

    public MutableKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public MutableKeyValuePair()
    {
    }
}