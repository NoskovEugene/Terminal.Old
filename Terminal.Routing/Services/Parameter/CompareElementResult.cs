namespace Terminal.Routing.Services.Parameter;

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