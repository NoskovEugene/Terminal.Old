using SemanticAnalyzer.Models;

namespace SemanticAnalyzer;

public class SemanticService : ISemanticService
{
    private List<ParserChainElement> ParsersChain { get; set; } = new();

    public void AddParser<T>(int priority) where T : class, IParser
    {
        ParsersChain.Add(new (priority, typeof(T)));
    }

    public void AddParser<T>() where T : class, IParser
    {
        ParsersChain = ParsersChain.OrderBy(x => x.Priority).ToList();
        AddParser<T>(ParsersChain.Count == 0 ? 0 : ParsersChain[^1].Priority + 1);
    }

    public void RemoveParser<T>() where T : class, IParser
    {
        var parser = ParsersChain.FirstOrDefault(x => x.ParserType == typeof(T));
        if (parser != null)
        {
            ParsersChain.Remove(parser);
        }
    }

    public void ResetParsers()
    {
        ParsersChain.Clear();
    }

    public ParsingContext ParseInputLine(string input)
    {
        ParsersChain = ParsersChain.OrderBy(x => x.Priority).ToList();
        var context = new ParsingContext()
        {
            UnparsedLine = input,
            CurrentStep = input
        };
        ParsersChain.ForEach(x =>
        {
            x.Instance ??= (IParser)Activator.CreateInstance(x.ParserType);
            x.Instance.Parse(ref context);
        });
        return context;
    }
}