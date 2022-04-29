using Castle.MicroKernel.Registration;
using Castle.Windsor;
using UI.RequestService;

namespace Terminal.Core.Helpers;

public static class UiHelper
{
    public static void RegisterUiServices(this WindsorContainer container)
    {
        container.Register(Component.For<UserRequestService>()
            .ImplementedBy<UserRequestService>());
        
    }
}