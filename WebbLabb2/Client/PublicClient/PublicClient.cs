namespace WebbLabb2.Client.PublicClient;

public class PublicClient
{
    public HttpClient Client { get; }

    public PublicClient(HttpClient httpClient)
    {
        Client = httpClient;
    }
}