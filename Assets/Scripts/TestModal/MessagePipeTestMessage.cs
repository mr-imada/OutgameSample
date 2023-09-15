/// <summary>
/// MessagePipeを利用して全モーダルに通知されるメッセージ
/// </summary>
public class MessagePipeTestMessage
{
    public readonly int Count;
    
    public MessagePipeTestMessage(int count)
    {
        Count = count;
    }
}