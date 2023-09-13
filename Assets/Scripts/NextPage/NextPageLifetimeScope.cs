using UnityEngine;
using VContainer;
using VContainer.Unity;

public class NextPageLifetimeScope : LifetimeScope
{
    [SerializeField] private NextPageView _view;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<NextPageLifecycle>(Lifetime.Singleton);
        builder.RegisterComponent(_view);
        builder.Register<NextPageUseCaseMock>(Lifetime.Singleton);
    }
}