using System.Reflection;
using Terminal.SharedModels.Attributes.UtilityAttributes;

namespace Terminal.Routing.Extensions;

public static class TypeExtensions
{
    public static bool IsUtility(this Type type, out UtilityAttribute attribute)
    {
        attribute = type.GetCustomAttribute<UtilityAttribute>();
        return attribute != null;
    }
}