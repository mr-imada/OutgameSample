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
    private readonly CountParameter _parameter;

    /// <summary>
    /// モーダルの重なり回数のパラメータ
    /// </summary>
    public class CountParameter
    {
        public readonly int ModalCount;

        public CountParameter(int count)
        {
            ModalCount = count;
        }
    }

    [Inject]
    public TestModalLifecycle(TestModalView view, ModalManager modalManager, CountParameter parameter) : base(view)
    {
        _view = view;
        _modalManager = modalManager;
        _parameter = parameter;
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

        _view.OnNext.Subscribe(_ =>
        {
            var nextParameter = new CountParameter(_parameter.ModalCount + 1);
            _modalManager.Push(new TestModalBuilder(nextParameter), cancellationToken: ExitCancellationToken).Forget();
        });

        _view.OnClose.Subscribe(_ => { _modalManager.Pop(true, cancellationToken: ExitCancellationToken).Forget(); });
    }
}