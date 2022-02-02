using System.Reflection;
using Routing.Models;

namespace Routing.Scanner;

public interface IAssemblyScanner
{
    List<Utility> Scan(Assembly assembly);
}