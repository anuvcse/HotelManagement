using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotelManagement_User.Services
{
    public class HttpService:IHttpService
    {
        private HttpClient _httpClient;
        private ILogger<HttpService> _logger;
        public HttpService(HttpClient httpClient,ILogger<HttpService> logger )
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger= logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T> PostAsync<T>(string url,StringContent content)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await _httpClient.PostAsync(url, content);
            if (Res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>( Res.Content.ReadAsStringAsync().Result);
            }
            else
            {
                _logger.LogError("Error in hitting the api with status code" + Res.StatusCode);
                throw new HttpRequestException("Error in hitting the api with status code" + Res.StatusCode);
            }
        }
        public async Task<T> GetAsync<T>(string url)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await _httpClient.GetAsync(url);
            if (Res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(Res.Content.ReadAsStringAsync().Result);
            }
            else
            {
                _logger.LogError("Error in hitting the api with status code" + Res.StatusCode);
                throw new HttpRequestException("Error in hitting the api with status code" + Res.StatusCode);
            }
        }
    }
}
