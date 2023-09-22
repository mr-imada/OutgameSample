using VContainer;
using ScreenSystem.Page;
using ScreenSystem.Modal;
using ScreenSystem.Attributes;
using Cysharp.Threading.Tasks;
using System.Threading;
using MessagePipe;
using UniRx;

[AssetName("TestPage")]
public class TestPageLifecycle : LifecyclePageBase
{
    private readonly TestPageView _view;
    private readonly PageEventPublisher _publisher;
    private readonly ModalManager _modalManager;
    private readonly NextPageUseCase _nextPageUseCase;
    private ISubscriber<MessagePipeTestMessage> _testMessageSubscriber;

    [Inject]
    public TestPageLifecycle(TestPageView view, PageEventPublisher publisher, ModalManager modalManager, NextPageUseCase nextPageUseCase, ISubscriber<MessagePipeTestMessage> testMessageSubscriber) : base(view)
    {
        _view = view;
        _publisher = publisher;
        _modalManager = modalManager;
        _nextPageUseCase = nextPageUseCase;
        _testMessageSubscriber = testMessageSubscriber;
    }

    protected override UniTask WillPushEnterAsync(CancellationToken cancellationToken)
    {
        var testModel = new TestPageModel();
        _view.SetView(testModel);
        return UniTask.CompletedTask;
    }

    public override void DidPushEnter()
    {
        base.DidPushEnter();
        _view.OnClickPage.Subscribe(_ => UniTask.Void(async () =>
        {
            // 通信を行い、通信結果を渡して次の画面を開く
            var parameter = await _nextPageUseCase.DoConnect(cancellationToken: ExitCancellationToken);
            _publisher.SendPushEvent(new NextPageBuilder(parameter));
        }));
        _view.OnClickModal.Subscribe(_ =>
        {
            // 通信を行わずにパラメータだけを渡して次の画面を開く
            var countParameter = new TestModalLifecycle.CountParameter(1);
            _modalManager.Push(new TestModalBuilder(countParameter), cancellationToken: ExitCancellationToken).Forget();
        });
        _testMessageSubscriber.Subscribe(m =>
        {
            _view.UpdateModalCount(m.Count);
        }).AddTo(DisposeCancellationToken);
    }
}