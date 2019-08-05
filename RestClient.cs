using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CmdParser
{

    public static class RestClient
    {
        private static readonly HttpClient client = new HttpClient();
        internal static readonly string baseUrl = "http://localhost:5590/api/v1";

        internal static async Task HttpGetter(string url)
        {
            // Console.WriteLine(url);
            try
            {
                var response = await client.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Length > 0)
                {
                    var jsonFormatted = JToken.Parse(responseBody).ToString(Formatting.Indented);
                    Console.WriteLine(jsonFormatted);
                }
                response.EnsureSuccessStatusCode(); //?

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (JsonReaderException jex)
            {
                Console.WriteLine(jex.Message);
            }

        }

        internal static async Task HttpDeleter(string url)
        {
            // Console.WriteLine(url);
            try
            {
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Length > 0)
                {
                    var jsonFormatted = JToken.Parse(responseBody).ToString(Formatting.Indented);
                    Console.WriteLine(jsonFormatted);
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (JsonException jex)
            {
                Console.WriteLine(jex.Message);
            }
        }

        internal static async Task HttpPoster(string url, string content)
        {
            // Console.WriteLine(url);
            try
            {
                var stringContent = new StringContent(content, Encoding.UTF8, @"application/json");
                var response = await client.PostAsync(url, stringContent);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Length > 0)
                {
                    var jsonFormatted = JToken.Parse(responseBody).ToString(Formatting.Indented);
                    Console.WriteLine(jsonFormatted);
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (JsonException jex)
            {
                Console.WriteLine(jex.Message);
            }
        }

    }
}
