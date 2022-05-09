using Terminal.SharedModels.Attributes.UtilityAttributes;

namespace Terminal.SystemCommands.Attributes;

public class SystemUtilityAttribute : UtilityAttribute
{
    public SystemUtilityAttribute() : base("sys")
    {
    }
}