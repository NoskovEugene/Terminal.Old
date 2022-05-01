namespace Terminal.Routing.Services.Parameter.ParameterAnalyze;

public class CompareElementResult
{
    public CompareElementResult(bool success, Type arrayType)
    {
        Success = success;
        ArrayType = arrayType;
    }

    public bool Success { get;  }
    
    public Type ArrayType { get;  }
}