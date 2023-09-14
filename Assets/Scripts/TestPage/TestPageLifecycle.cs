using VContainer;
using ScreenSystem.Page;
using ScreenSystem.Modal;
using ScreenSystem.Attributes;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

[AssetName("TestPage")]
public class TestPageLifecycle : LifecyclePageBase
{
    private readonly TestPageView _view;
    private readonly PageEventPublisher _publisher;
    private readonly ModalManager _modalManager;
    private readonly NextPageUseCase _nextPageUseCase;
    private readonly TestModalUseCase _testModalUseCase;

    [Inject]
    public TestPageLifecycle(TestPageView view, PageEventPublisher publisher, ModalManager modalManager, NextPageUseCase nextPageUseCase, TestModalUseCase testModalUseCase) : base(view)
    {
        _view = view;
        _publisher = publisher;
        _modalManager = modalManager;
        _nextPageUseCase = nextPageUseCase;
        _testModalUseCase = testModalUseCase;
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
            var parameter = await _nextPageUseCase.DoConnect(cancellationToken: PageExitCancellationToken);
            _publisher.SendPushEvent(new NextPageBuilder(parameter));
        }));
        _view.OnClickModal.Subscribe(_ => UniTask.Void(async () =>
        {
            var parameter = await _testModalUseCase.DoConnect(cancellationToken: PageExitCancellationToken);
            _modalManager.Push(new TestModalBuilder(parameter), cancellationToken: PageExitCancellationToken).Forget();
        }));
    }
}