using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Text.Json;
using System.Diagnostics.Metrics;

namespace VK_Library
{
    public class Response<T>
    {
        public string rawContent { get; set; }
        public T response { get; set; }
    }

    public class Utility
    {
        private static string ACCESS_TOKEN = "f8750551f8750551f87505517dfb61819cff875f87505519cfcb539f40f0182e5e0379d";
        private static string Version = "5.131";

        private static HttpClient client = new HttpClient();

        public static Task<HttpResponseMessage> VKGet(string method, Dictionary<string, string> parameters)
        {
            var builder = new UriBuilder($"https://api.vk.com/method/{method}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_token"] = ACCESS_TOKEN;
            query["v"] = Version;
            foreach (var key in parameters.Keys)
            {
                query[key] = parameters[key];
            }
            builder.Query = query.ToString();
            string url = builder.ToString();
            return client.GetAsync(url);
        }

        public static string PrettyJson(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return JsonSerializer.Serialize(jsonElement, options);
        }

        public static async Task<Response<VKDictResponse<VKItemsResponse<VKUserInfo>>>> FetchMembersInfo(string groupId)
        {
            HttpResponseMessage response = await VKGet("groups.getMembers", new Dictionary<string, string>
            {
                ["group_id"] = groupId,
                ["fields"] = "photo_100",
                ["count"] = "100",
                ["lang"] = "ru"
            });

            var content = await response.Content.ReadAsStringAsync();

            var itemsResponse = JsonSerializer.Deserialize<VKDictResponse<VKItemsResponse<VKUserInfo>>>(content);

            return new Response<VKDictResponse<VKItemsResponse<VKUserInfo>>>
            {
                rawContent = content,
                response = itemsResponse
            };
        }

        public static async Task<Response<VKListResponse<VKFullUser>>> FetchFullUserInfo(string user_id)
        {
            HttpResponseMessage response = await VKGet("users.get", new Dictionary<string, string>
            {
                ["user_ids"] = user_id,
                ["fields"] = "games,photo_max_orig",
                ["lang"] = "ru",
            });

            var content = await response.Content.ReadAsStringAsync();

            var itemsResponse = JsonSerializer.Deserialize<VKListResponse<VKFullUser>>(content);

            return new Response<VKListResponse<VKFullUser>>
            {
                rawContent = content,
                response = itemsResponse
            };
        }
    }
}
