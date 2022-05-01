using Terminal.Common.Extensions.List;
using Terminal.Common.MapService;
using Terminal.Common.MapService.Helpers;
using Terminal.Routing.Services.ParameterMappingService;
using Terminal.SemanticAnalyzer.Models;

namespace Terminal.Routing.Services.Parameter.ParameterAnalyze;

public class ParameterAnalyzeService : IParameterAnalyzeService
{
    private Map<Type> _typesMapping;

    public ParameterAnalyzeService()
    {
        _typesMapping = new MapConfigurator<Type>().UseProfile<DefaultTypesMapProfile>().Build();
    }

    public bool CheckType<TFrom, TTo>()
    {
        return _typesMapping.ExistPath<TFrom, TTo>();
    }
    
    public IEnumerable<object> PrepareParsedParameters(ParsingContext context)
    {
        var resultList = new List<object>();
        context.ParsedParameters.Foreach(x =>
        {
            if (x.ParameterTypeEnum == ParsedParameterTypeEnum.Value)
            {
                resultList.Add(x.Value);
            }
            else
            {
                var array = ((List<ParsedParameter>)x.Value).Select(x => x.Value).ToArray();
                var destination = Array.CreateInstance(x.PossibleParameterType, array.Length);
                Array.Copy(array, destination, array.Length);
                resultList.Add(destination);
            }
        });
        return resultList;
    }

    public void ChangeParametersToPossibleType(ParsingContext context)
    {
        foreach (var parsedParameter in context.ParsedParameters)
        {
            AnalyzeParameter(parsedParameter);
        }
    }

    private Type AnalyzeParameter(ParsedParameter parameter)
    {
        return parameter.ParameterTypeEnum == ParsedParameterTypeEnum.Value
            ? AnalyzeValueElement(parameter)
            : AnalyzeArrayParameter(parameter);
    }

    private Type AnalyzeValueElement(ParsedParameter parameter)
    {
        var type = GetPossibleTypeWithCast(parameter.Value.ToString(), out var result);
        parameter.Value = result;
        parameter.PossibleParameterType = type;
        return type;
    }

    private Type AnalyzeArrayParameter(ParsedParameter parameter)
    {
        var parsedParameterArray = ((IEnumerable<ParsedParameter>)parameter.Value).ToList();
        var arrayType = AnalyzeParameter(parsedParameterArray[0]);
        for (var i = 1; i < parsedParameterArray.Count; i++)
        {
            var currentElementType = AnalyzeParameter(parsedParameterArray[i]);
            var compareResult = CompareArrayElements(arrayType, currentElementType);
            if (compareResult.Success)
            {
                arrayType = compareResult.ArrayType;
            }
            else
            {
                throw new Exception("Inconsistent array");
            }

            foreach (var parsedParameter in parsedParameterArray)
            {
                parsedParameter.Value = Convert.ChangeType(parsedParameter.Value, arrayType);
                parsedParameter.PossibleParameterType = arrayType;
            }
        }

        parameter.PossibleParameterType = arrayType;
        return arrayType;
    }

    private CompareElementResult CompareArrayElements(Type leftElement, Type rightElement)
    {
        if (leftElement == typeof(int) && rightElement == typeof(double) ||
            leftElement == typeof(double) && rightElement == typeof(int))
        {
            return new CompareElementResult(true, typeof(double));
        }

        return leftElement == rightElement
            ? new CompareElementResult(true, leftElement)
            : new CompareElementResult(false, null);
    }

    private Type GetPossibleTypeWithCast(string value, out object castedValue)
    {
        castedValue = value;
        value = value.ToLower();
        if (byte.TryParse(value, out var byteVal))
        {
            castedValue = byteVal;
            return typeof(byte);
        }
        if (int.TryParse(value, out var intVal))
        {
            castedValue = intVal;
            return typeof(int);
        }
        if (double.TryParse(value, out var doubleVal))
        {
            castedValue = doubleVal;
            return typeof(double);
        }
        if (bool.TryParse(value, out var boolVal))
        {
            castedValue = boolVal;
            return typeof(double);
        }

        if (DateTime.TryParse(value, out var dateVal))
        {
            castedValue = dateVal;
            return typeof(DateTime);
        }

        return typeof(string);
    }
}