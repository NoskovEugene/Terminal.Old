using Terminal.Common.MapService;
using Terminal.Common.MapService.Helpers;

namespace Terminal.Routing.Services.ParameterMappingService;

public class DefaultTypesMapProfile : MapProfile<Type>
{
    public DefaultTypesMapProfile()
    {
        CreatePath(x=> x.Source<byte>().Target<short>());
        CreatePath(x=> x.Source<short>().Target<int>().Target<float>());
        CreatePath(x=> x.Source<int>().Target<float>().Target<long>());
        CreatePath(x => x.Source<long>().Target<double>());
        CreatePath(x=> x.Source<float>().Target<double>());
    }
}