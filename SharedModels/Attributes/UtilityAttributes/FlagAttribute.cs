namespace SharedModels.Attributes.UtilityAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class FlagAttribute : Attribute
{
    public string FlagName { get; set; }

    public FlagAttribute(string flagName)
    {
        FlagName = flagName;
    }
}