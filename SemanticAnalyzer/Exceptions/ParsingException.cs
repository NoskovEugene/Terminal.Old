namespace SemanticAnalyzer.Exceptions;

public class ParsingException : Exception
{
    public ParsingException(string parsingStep, string message) :
        base($"Some errors at parsing step.\r\nStep - {parsingStep}\r\nMessage - {message}")
    {
    }
}