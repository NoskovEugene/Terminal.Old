using System.Reflection;
using Terminal.SharedModels.Models.Routing.Scanner;

namespace Terminal.Routing.Scanner;

public interface IAssemblyScanner
{
    List<Utility> ScanAssembly(Assembly assembly);
}