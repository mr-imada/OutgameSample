using VContainer;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TestModalUseCaseMock
{
    [Inject]
    public TestModalUseCaseMock() { }

    public async UniTask<TestModalLifecycle.NetworkParameter> DoConnect(CancellationToken cancellationToken)
    {
        // ここでネットワーク通信を行う
        await UniTask.Yield();
        return new TestModalLifecycle.NetworkParameter("TestModal");
    }
}
