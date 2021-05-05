using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using RestAPITests.Resources;
using System.Net;
using RestSharp.Serialization.Json;

namespace Tests
{
    internal class BaseTest
    {
        IRestClient client;
        IRestRequest request;
        IRestResponse response;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(Resources.URL);
        }

        public void CreateGetRequest(String resource)
        {
            request = new RestRequest(resource, Method.GET);
        }

        public void CreateGetRequest(String resource, String content)
        {
            request = new RestRequest($"{resource}/{content}", Method.GET);
        }

        public void CreateResponse()
        {
            response = client.Execute(request);
        }

        public HttpStatusCode GetStatusCode()
        {
            return response.StatusCode;
        }

        public String GetResponseContentType()
        {
            return response.ContentType;
        }

        public String GetResponseContent()
        {
            return response.Content;
        }

        public T Deserialize<T>()
        {
            return new JsonSerializer().Deserialize<T>(response);
        }
    }
}
