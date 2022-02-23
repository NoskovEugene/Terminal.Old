using SemanticAnalyzer;
using SemanticAnalyzer.Models;

namespace Routing;

public class Router : IRouter
{
    protected ISemanticService SemanticService { get; set; }
    
    public Router(ISemanticService semanticService)
    {
        SemanticService = semanticService;
    }
    
    
}