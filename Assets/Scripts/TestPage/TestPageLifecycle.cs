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
    private readonly TestPageUseCaseMock _useCase;

    public class NetworkParameter
    {
        public readonly string Message;

        public NetworkParameter(string message)
        {
            Message = message;
        }
    }

    [Inject]
    public TestPageLifecycle(TestPageView view, PageEventPublisher publisher, ModalManager modalManager, TestPageUseCaseMock useCase) : base(view)
    {
        _view = view;
        _publisher = publisher;
        _modalManager = modalManager;
        _useCase = useCase;
    }

    protected override async UniTask WillPushEnterAsync(CancellationToken cancellationToken)
    {
        var parameter = await _useCase.DoConnect(cancellationToken);
        var testModel = new TestPageModel(parameter);
        _view.SetView(testModel);
    }

    public override void DidPushEnter()
    {
        base.DidPushEnter();
        _view.OnClickPage.Subscribe(_ =>
        {
            _publisher.SendPushEvent(new NextPageBuilder());
        });
        _view.OnClickModal.Subscribe(_ =>
        {
            _modalManager.Push(new TestModalBuilder(), cancellationToken: default).Forget();
        });
    }
}