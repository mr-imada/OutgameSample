using ScreenSystem.Page;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

namespace ScreenSystem.VContainerExtension
{
    public static class RegisterPageSystemExtension
    {
        public static void RegisterPageSystem(this IContainerBuilder builder, PageContainer pageContainer)
        {
            builder.Register<PageManager>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(pageContainer);

            builder.Register<PageEventPublisher>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}