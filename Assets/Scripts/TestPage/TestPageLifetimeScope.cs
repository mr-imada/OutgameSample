using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestPageLifetimeScope : LifetimeScope
{
    [SerializeField] private TestPageView _view;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<TestPageLifecycle>(Lifetime.Singleton);
        builder.RegisterComponent(_view);
        builder.Register<TestModalLifecycle>(Lifetime.Singleton);
        builder.Register<TestPageUseCaseMock>(Lifetime.Singleton);
    }
}