/// <summary>
/// 実際のリクエストはこのクラスを継承して定義する
/// </summary>
public abstract class HttpRequest
{
    // 通信するURL
    public abstract string Path { get; }
    
    /// <summary>
    /// 成功/中断/失敗の通信自体の結果を定義
    /// </summary>
    public abstract record Result()
    {
        public bool IsSuccess() => this is Success;
        public bool IsCanceled() => this is Canceled;
        public bool IsFailed() => this is Failed;
    }

    public record Success() : Result;

    public record Canceled() : Result;

    public record Failed() : Result;
}