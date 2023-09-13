using VContainer;
using ScreenSystem.Page;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using ScreenSystem.Attributes;

[AssetName("NextPage")]
public class NextPageLifecycle : LifecyclePageBase
{
    private readonly NextPageView _view;
    private readonly PageEventPublisher _publisher;
    private readonly NextPageUseCaseMock _useCase;

    public class NetworkParameter
    {
        public readonly string Message;

        public NetworkParameter(string message)
        {
            Message = message;
        }
    }

    [Inject]
    public NextPageLifecycle(NextPageView view, PageEventPublisher publisher, NextPageUseCaseMock useCase) : base(view)
    {
        _view = view;
        _publisher = publisher;
        _useCase = useCase;
    }

    protected override async UniTask WillPushEnterAsync(CancellationToken cancellationToken)
    {
        var parameter = await _useCase.DoConnect(cancellationToken);
        var NextModel = new NextPageModel(parameter);
        _view.SetView(NextModel);
        await UniTask.Yield();
    }

    public override void DidPushEnter()
    {
        base.DidPushEnter();

        _view.OnClickReturn.Subscribe(_ =>
        {
            _publisher.SendPopEvent();
        });
    }
}