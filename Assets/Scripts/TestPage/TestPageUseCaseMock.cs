using VContainer;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TestPageUseCaseMock
{
    [Inject]
    public TestPageUseCaseMock() { }

    public async UniTask<TestPageLifecycle.NetworkParameter> DoConnect(CancellationToken cancellationToken)
    {
        // ここでネットワーク通信を行う
        await UniTask.Yield();
        return new TestPageLifecycle.NetworkParameter("TestPage");
    }
}
