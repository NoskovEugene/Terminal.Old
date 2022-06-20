namespace Terminal.SharedModels.Attributes.UtilityAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class UtilityAttribute : Attribute
{
    public string UtilityName { get; set; }
    
    public UtilityAttribute(string utilityName)
    {
        UtilityName = utilityName;
    }
}