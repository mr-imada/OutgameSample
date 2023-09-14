using VContainer;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TestModalUseCase
{
    [Inject]
    public TestModalUseCase() { }

    public async UniTask<TestModalLifecycle.NetworkParameter> DoConnect(CancellationToken cancellationToken)
    {
        // ここでネットワーク通信を行う
        await UniTask.Yield();
        return new TestModalLifecycle.NetworkParameter("TestModal");
    }
}
