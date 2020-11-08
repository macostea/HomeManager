using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class Utilities
    {
        public static async Task<T> ExecuteRestRequest<T>(Uri BaseURL, RestRequest request) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = BaseURL
            };
            var response = await client.ExecuteTaskAsync(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
            return JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
        }
    }
}
