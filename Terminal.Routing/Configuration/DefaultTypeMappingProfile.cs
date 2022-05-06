using Terminal.Common.MapService;
using Terminal.Common.MapService.Helpers;

namespace Terminal.Routing.Configuration;

public class DefaultTypeMappingProfile : MapProfile<Type>
{
    public DefaultTypeMappingProfile()
    {
        CreatePath(x=> x.Source<bool>().Target<byte>());
        CreatePath(x=> x.Source<byte>().Target<sbyte>().Target<short>());
        CreatePath(x=> x.Source<short>().Target<ushort>().Target<int>().Target<float>());
        CreatePath(x=> x.Source<int>().Target<uint>().Target<long>().Target<float>());
        CreatePath(x=> x.Source<long>().Target<ulong>().Target<double>());
        CreatePath(x=> x.Source<float>().Target<double>());
    }
}