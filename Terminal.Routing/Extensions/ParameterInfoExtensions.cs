using System.Reflection;
using Terminal.SharedModels.Attributes.UtilityAttributes;

namespace Terminal.Routing.Extensions;

public static class ParameterInfoExtensions
{
    public static bool IsFlag(this ParameterInfo info, out FlagAttribute flagAttribute)
    {
        flagAttribute = info.GetCustomAttribute<FlagAttribute>();
        return flagAttribute != null;
    }
}