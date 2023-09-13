using ScreenSystem.Modal;
using UnityScreenNavigator.Runtime.Core.Modal;
using VContainer;

namespace ScreenSystem.VContainerExtension
{
    public static class RegisterModalSystemExtension
    {
        public static void RegisterModalSystem(this IContainerBuilder builder, ModalContainer modalContainer)
        {
            builder.Register<ModalManager>(Lifetime.Singleton)
                .WithParameter(modalContainer);
        }
    }
}