using System.Diagnostics.Tracing;

namespace Terminal.Common.MapService.Helpers;

public static class MapHelper
{
    public static bool ExistPath<TFrom, TTo>(this Map<Type> map)
    {
        return map.ExistPath(typeof(TFrom), typeof(TTo));
    }
}