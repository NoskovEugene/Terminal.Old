using System.Reflection;
using SharedModels.Attributes.UtilityAttributes;

namespace Routing.Extensions;

public static class TypeExtensions
{
    public static bool IsUtility(this Type type, out UtilityAttribute attribute)
    {
        attribute = type.GetCustomAttribute<UtilityAttribute>();
        return attribute != null;
    }
}