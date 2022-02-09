using System.Reflection;
using SharedModels.Attributes.UtilityAttributes;

namespace Routing.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsCommand(this MethodInfo info, out CommandAttribute attribute)
    {
        attribute = info.GetCustomAttribute<CommandAttribute>();
        return attribute != null;
    }
}