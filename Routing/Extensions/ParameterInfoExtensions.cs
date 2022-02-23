using System.Reflection;
using SharedModels.Attributes.UtilityAttributes;

namespace Routing.Extensions;

public static class ParameterInfoExtensions
{
    public static bool IsFlag(this ParameterInfo info, out FlagAttribute flagAttribute)
    {
        flagAttribute = info.GetCustomAttribute<FlagAttribute>();
        return flagAttribute != null;
    }
}