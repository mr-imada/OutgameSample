using MessagePipe;
using ScreenSystem.Page;
using ScreenSystem.VContainerExtension;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] UnityScreenNavigator.Runtime.Core.Page.PageContainer _container;
    [SerializeField] UnityScreenNavigator.Runtime.Core.Modal.ModalContainer _modalContainer;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterPageSystem(_container);
        builder.RegisterModalSystem(_modalContainer);
        builder.Register<IHttpClient>(_ => new HttpClient(), Lifetime.Singleton);

        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<MessagePipeTestMessage>(options);
        builder.RegisterEntryPoint<TestEntryPoint>();
    }

    private class TestEntryPoint : IStartable
    {
        private readonly PageEventPublisher _publisher;

        public TestEntryPoint(PageEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Start()
        {
            _publisher.SendPushEvent(new TestPageBuilder());
        }
    }
}