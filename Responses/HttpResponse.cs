using System.Net;

namespace HttpRequestHandler.Responses
{
    /// <summary>
    /// Class <c>HttpResponse</c> serves as a response for the <c>HttpRequestHandler</c> class.
    /// </summary>
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; } = default!;
    }
}
