using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestModalLifetimeScope : LifetimeScope
{
    [SerializeField] private TestModalView _view;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.Register<TestModalLifecycle>(Lifetime.Singleton);
        builder.RegisterComponent(_view);
        builder.Register<TestModalUseCaseMock>(Lifetime.Singleton);
    }
}