namespace Terminal.SharedModels.Attributes.UtilityAttributes;
[AttributeUsage(AttributeTargets.Method)]
public class ParameterAttribute : Attribute
{
    public string ParameterName { get; set; }

    public ParameterAttribute(string parameterName)
    {
        ParameterName = parameterName;
    }
}