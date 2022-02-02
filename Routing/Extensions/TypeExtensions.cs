using SharedModels.Attributes.UtilityAttributes;
using System.Reflection;

namespace Routing.Extensions;

public static class TypeExtensions
{
    public static bool IsUtility(this Type type, out UtilityAttribute attribute)
    {
        attribute = type.GetCustomAttribute<UtilityAttribute>();
        return attribute != null;
    }

    public static bool IsCommand(this MethodInfo methodInfo, out CommandAttribute commandAttribute)
    {
        commandAttribute = methodInfo.GetCustomAttribute<CommandAttribute>();
        return commandAttribute != null;
    }
}