using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using ScreenSystem.Attributes;
using ScreenSystem.Modal;
using UniRx;
using VContainer;

[AssetName("TestModal")]
public class TestModalLifecycle : LifecycleModalBase
{
    private readonly TestModalView _view;
    private readonly TestModalUseCaseMock _useCase;
    private readonly ModalManager _modalManager;

    public class NetworkParameter
    {
        public readonly string Message;

        public NetworkParameter(string message)
        {
            Message = message;
        }
    }

    [Inject]
    public TestModalLifecycle(
        TestModalView view,
        TestModalUseCaseMock useCase,
        ModalManager modalManager
        ) : base(view)
    {
        _view = view;
        _useCase = useCase;
        _modalManager = modalManager;
    }

    protected override async UniTask WillPushEnterAsync(CancellationToken cancellationToken)
    {
        var parameter = await _useCase.DoConnect(cancellationToken);
        var testModel = new TestModalModel(parameter);
        _view.SetView(testModel);
        await UniTask.Yield();
    }

    public override void DidPushEnter()
    {
        base.DidPushEnter();

        _view.OnClose.Subscribe(_ =>
        {
            _modalManager.Pop(true, cancellationToken: default).Forget();
        });
    }
}
