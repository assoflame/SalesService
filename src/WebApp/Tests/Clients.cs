namespace Tests
{
    public class Clients
    {
        public HttpClient UnauthorizedClient { get; set; }
        public HttpClient AuthorizedClient { get; set; }
        public HttpClient AdminClient { get; set; }
    }
}
