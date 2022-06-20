namespace Terminal.SharedModels.Attributes.UtilityAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class CommandAttribute : Attribute
{
    public string CommandName { get; set; }

    public CommandAttribute(string commandName)
    {
        CommandName = commandName;
    }
}