using System;

[Serializable]
public class HttpResponseStatus
{
    /// <summary>
    /// 通信そのものが成功したか、サーバーから返される
    /// </summary>
    public int ok;

    public int error_code;
}

/// <summary>
/// 実際のレスポンスはこのクラスを継承して定義する
/// </summary>
public abstract class HttpResponse
{
    public HttpResponseStatus status;
}