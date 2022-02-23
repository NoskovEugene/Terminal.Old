using System.Reflection;
using SharedModels.Models.Routing.Scanner;

namespace Routing.Scanner;

public interface IAssemblyScanner
{
    List<Utility> ScanAssembly(Assembly assembly);
}