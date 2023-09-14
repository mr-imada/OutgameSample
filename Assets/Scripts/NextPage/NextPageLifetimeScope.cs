using ScreenSystem.VContainerExtension;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class NextPageLifetimeScope : LifetimeScopeWithParameter<NextPageLifecycle.NetworkParameter>
{
    [SerializeField] private NextPageView _view;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.Register<NextPageLifecycle>(Lifetime.Singleton);
        builder.RegisterComponent(_view);
    }
}