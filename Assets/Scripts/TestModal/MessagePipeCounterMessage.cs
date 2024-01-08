/// <summary>
/// MessagePipeを利用して全モーダルに通知されるメッセージ
/// </summary>
public class MessagePipeCounterMessage
{
    public readonly int Count;
    
    public MessagePipeCounterMessage(int count)
    {
        Count = count;
    }
}