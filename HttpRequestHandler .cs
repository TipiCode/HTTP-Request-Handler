using HttpRequestHandler.Responses;
using HttpRequestHandler.Utils;
using System.Net.Http.Headers;
using System.Text;

namespace Tipi.Tools
{
    public class HttpRequestHandler : IDisposable
    {
        private readonly HttpClient _client;
        private HttpResponseMessage? _response;
        private StringContent? _content;

        public HttpRequestHandler(string? bearerToken = null, Dictionary<string, string>? headers = null)
        {
            _client = new HttpClient();

            if(headers != null)
                foreach(var header in headers)
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);

            if (!String.IsNullOrEmpty(bearerToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }
        public async Task<HttpResponse> ExecuteAsync(string method, string endpoint, string? body = null)
        {
            _response = new HttpResponseMessage(); //Initialize Response object
            if (body != null) //Check if the execute request need a base model for the body parameter
                SerializeBody(body);

            if (method == HttpMethods.Post)
                _response = await _client.PostAsync(endpoint, _content);

            else if (method == HttpMethods.Patch)
                _response = await _client.PatchAsync(endpoint, _content);

            else if (method == HttpMethods.Put)
                _response = await _client.PutAsync(endpoint, _content);

            else if (method == HttpMethods.Get)
                _response = await _client.GetAsync(endpoint);

            else if (method == HttpMethods.Delete)
                _response = await _client.DeleteAsync(endpoint);

            return new HttpResponse
            {
                StatusCode = _response.StatusCode,
                Body = await _response.Content.ReadAsStringAsync()
            };
        }

        private void SerializeBody(string jsonObject)
        {
            _content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
        }
        public void Dispose()
        {
            _client.Dispose();
            if (_response != null)
                _response.Dispose();
            if (_content != null)
                _content.Dispose();
        }
    }
}