using System.Net;

namespace HttpRequestHandler.Responses
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; } = default!;
    }
}
