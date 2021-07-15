using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopListDemo.Client.API
{
    public static class HttpResponseExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = response.Content == null
                    ? ""
                    : await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"{response.ReasonPhrase} - {responseContent}");
            }
        }
    }
}
