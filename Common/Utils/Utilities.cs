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
            var response = await client.ExecuteAsync<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
            return response.Data;
        }

        public static async Task ExecuteRestRequest(Uri BaseURL, RestRequest request)
        {
            var client = new RestClient
            {
                BaseUrl = BaseURL
            };
            var response = await client.ExecuteAsync(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exception = new ApplicationException(message, response.ErrorException);
                throw exception;
            }
        }
    }
}
