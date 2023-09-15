using System.Threading;
using Cysharp.Threading.Tasks;
using ScreenSystem.Attributes;
using ScreenSystem.Modal;
using UniRx;
using VContainer;

[AssetName("TestModal")]
public class TestModalLifecycle : LifecycleModalBase
{
    private readonly TestModalView _view;
    private readonly ModalManager _modalManager;
    private readonly NetworkParameter _parameter;
    private readonly TestModalUseCase _testModalUseCase;

    public class NetworkParameter
    {
        public readonly string Message;

        public NetworkParameter(string message)
        {
            Message = message;
        }
    }

    [Inject]
    public TestModalLifecycle(TestModalView view, ModalManager modalManager, NetworkParameter parameter, TestModalUseCase testModalUseCase) : base(view)
    {
        _view = view;
        _modalManager = modalManager;
        _parameter = parameter;
        _testModalUseCase = testModalUseCase;
    }

    protected override UniTask WillPushEnterAsync(CancellationToken cancellationToken)
    {
        var testModel = new TestModalModel(_parameter);
        _view.SetView(testModel);
        return UniTask.CompletedTask;
    }

    public override void DidPushEnter()
    {
        base.DidPushEnter();

        _view.OnNext.Subscribe(_ => UniTask.Void(async () =>
        {
            var parameter = await _testModalUseCase.DoConnect(cancellationToken: ExitCancellationToken);
            _modalManager.Push(new TestModalBuilder(parameter), cancellationToken: ExitCancellationToken).Forget();
        }));

        _view.OnClose.Subscribe(_ =>
        {
            _modalManager.Pop(true, cancellationToken: ExitCancellationToken).Forget();
        });
    }
}
