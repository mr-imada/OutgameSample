using ScreenSystem.VContainerExtension;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestModalLifetimeScope : LifetimeScopeWithParameter<TestModalLifecycle.NetworkParameter>
{
    [SerializeField] private TestModalView _view;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.Register<TestModalLifecycle>(Lifetime.Singleton);
        builder.RegisterComponent(_view);
        builder.Register<TestModalUseCase>(Lifetime.Singleton);
    }
}