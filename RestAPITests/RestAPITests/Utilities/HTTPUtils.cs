using RestAPITests.Resources;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;
using System.Resources;

namespace RestAPITests.Utilities
{
    class HTTPUtils
    {
        static IRestClient client;
        static IRestRequest request;
        static IRestResponse response;
        readonly static ResourceManager resourceManager = confData.ResourceManager;

        public static void CreateClient()
        {
            client = new RestClient(resourceManager.GetString("URL"));
        }


        public static void CreateGetPostsRequest()
        {
            request = new RestRequest($"posts", Method.GET);
        }

        public static void CreatePostPostsRequest()
        {
            request = new RestRequest($"posts", Method.POST);
        }

        public static void CreateGetPostsRequestWithParam(string content)
        {
            request = new RestRequest($"posts/{content}", Method.GET);
        }

        public static void CreateGetUsersRequest()
        {
            request = new RestRequest($"users", Method.GET);
        }

        public static void CreateGetUsersRequestWithParam(string content)
        {
            request = new RestRequest($"users/{content}", Method.GET);
        }

        public static T CreateResponse<T>()
        {
            //не знаю или можно здесь упростить, потому что response нужна для других методов
            response = client.Execute(request);
            return new JsonSerializer().Deserialize<T>(response);
        }

        public static HttpStatusCode GetStatusCode()
        {
            return response.StatusCode;
        }

        public static string GetResponseContentType()
        {
            return response.ContentType;
        }

        public static string GetResponseContent()
        {
            return response.Content;
        }

        public static void AddJsonBodyToRequest<T>(T addedPost)
        {
            request.AddJsonBody(JsonConvertUtil.SerializeObj(addedPost));
        }
    }
}
