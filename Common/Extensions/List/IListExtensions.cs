﻿namespace Common.Extensions.List;

public static class ListExtensions
{
    public static void Foreach<T>(this IList<T> collection, Action<T> action)
    {
        if(collection == null)
            return;
        foreach (var x1 in collection)
        {
            action(x1);
        }
    }
}