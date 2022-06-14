using Serilog;
using Terminal.SharedModels.Attributes.UtilityAttributes;
using Terminal.SharedModels.Models.Routing.Scanner;
using Terminal.SystemCommands.Attributes;
using UI.RequestService;

namespace Terminal.SystemCommands;

[SystemUtility]
public class TestCommands
{
    private UserRequestService _userRequestService;

    private ILogger _logger;
    
    public TestCommands(UserRequestService userRequestService, ILogger logger)
    {
        _userRequestService = userRequestService;
        _logger = logger;
    }
    
    [Command("testuserrequest")]
    public void TestUserRequestService()
    {
        _logger.Information("---This is a UserRequestService test---");
        _logger.Information("Requesting line");
        var line = _userRequestService.RequestLine("Type line below");
        _logger.Information($"Typed line is '{line}'");
        _logger.Information("Request build-in");
        var boolVal = _userRequestService.RequestBuiltIn<bool>("Type bool below");
        _logger.Information($"Bool value is {boolVal}");
        var byteVal = _userRequestService.RequestBuiltIn<byte>("Type byte below");
        _logger.Information($"Byte value is {byteVal}");
        var intVal = _userRequestService.RequestBuiltIn<int>("Type int below");
        _logger.Information($"Int32 value is {intVal}");
        var doubleVal = _userRequestService.RequestBuiltIn<double>("Type double below");
        _logger.Information($"Double value is {doubleVal}");
        var collection = new List<string>
        {
            "First",
            "Second",
            "Third"
        };
        var selectedItem = _userRequestService.SelectItem(collection, "Select one item");
        _logger.Information($"Selected item is {selectedItem}");
        _logger.Information("---Test end---");
    }
}