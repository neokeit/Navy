using Newtonsoft.Json;
using RestSharp;

namespace Navy.Services.Helper
{
    public class RestApiHelper
    {
        public static T? SendRestApi<T>(string url)
        {
            var options = new RestClientOptions();
            var client = new RestClient(options);
            var request = new RestRequest(url);
            var response = client.Execute(request);
            if (response.Content != null)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            return default;
        }
    }
}
