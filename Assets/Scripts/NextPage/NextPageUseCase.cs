using VContainer;
using Cysharp.Threading.Tasks;
using System.Threading;

public class NextPageUseCase
{
    private readonly IHttpClient _httpClient;

    [Inject]
    public NextPageUseCase(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async UniTask<NextPageLifecycle.NetworkParameter> DoConnect(CancellationToken cancellationToken)
    {
        // リクエストを作って通信を行う
        var request = new NextPageNetworkRequest();
        var (result, response) = await _httpClient.Call<NextPageNetworkResponse>(request, cancellationToken);
        if (!result.IsSuccess())
        {
            // 本来はリトライ処理や中断処理などを行う
        }
        return new NextPageLifecycle.NetworkParameter(response.message);
    }
}
