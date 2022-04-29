using System.Reflection;
using Terminal.SharedModels.Attributes.UtilityAttributes;

namespace Terminal.Routing.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsCommand(this MethodInfo info, out CommandAttribute attribute)
    {
        attribute = info.GetCustomAttribute<CommandAttribute>();
        return attribute != null;
    }
}