using System;

[Serializable]
public class NextPageNetworkRequest : HttpRequest
{
    public override string Path => "https://hoge.com/nextpage";
}

[Serializable]
public class NextPageNetworkResponse : HttpResponse
{
    public string message = "Next Page";
}