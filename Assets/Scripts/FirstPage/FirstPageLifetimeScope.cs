using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FirstPageLifetimeScope : LifetimeScope
{
    [SerializeField] private FirstPageView _view;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<FirstPageLifecycle>(Lifetime.Singleton);
        builder.RegisterComponent(_view);
        builder.Register<TestModalLifecycle>(Lifetime.Singleton);
        builder.Register<NextPageUseCase>(Lifetime.Singleton);
    }
}