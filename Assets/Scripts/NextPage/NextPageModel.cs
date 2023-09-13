public class NextPageModel
{
    public readonly string NextMessage;

    public NextPageModel(NextPageLifecycle.NetworkParameter parameter)
    {
        NextMessage = parameter.Message;
    }
}