using System.Threading;
using Cysharp.Threading.Tasks;

public interface IHttpClient
{
    UniTask<(HttpRequest.Result result, T response)> Call<T>(HttpRequest request, CancellationToken token) where T : HttpResponse, new ();
}

/// <summary>
/// 実際に通信を行うクラス
/// </summary>
public class HttpClient : IHttpClient
{
    /// <summary>
    /// リクエストから通信を行い、レスポンスを取得する
    /// 成功/失敗とレスポンスを返す
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async UniTask<(HttpRequest.Result result, T response)> Call<T>(HttpRequest request, CancellationToken token) where T : HttpResponse, new()
    {
        // await UnityWebRequest.Get(); // 本来は通信処理を行う

        // 簡易版のため、通信処理を行わずに成功とデフォルトのオブジェクトを返す
        await UniTask.Yield();
        var mockResponse = new T();
        return (new HttpRequest.Success(), mockResponse);
    }
}