using VContainer;
using Cysharp.Threading.Tasks;
using System.Threading;

public class NextPageUseCaseMock
{
    [Inject]
    public NextPageUseCaseMock() { }

    public async UniTask<NextPageLifecycle.NetworkParameter> DoConnect(CancellationToken cancellationToken)
    {
        // ここでネットワーク通信を行う
        await UniTask.Yield();
        return new NextPageLifecycle.NetworkParameter("NextPage");
    }
}
