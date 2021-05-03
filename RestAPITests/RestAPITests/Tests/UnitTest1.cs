using NUnit.Framework;
using RestAPITests.DataEntities;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Test1()
        {
            RestClient client = new RestClient("https://jsonplaceholder.typicode.com");
            RestRequest request = new RestRequest("/posts", Method.GET);

            // act
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.IsSuccessful);
            Console.WriteLine((int)response.StatusCode);
            // assert
            //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
        }

        [Test]
        public void Test2()
        {
            IRestClient client = new RestClient("https://jsonplaceholder.typicode.com");
            IRestRequest request = new RestRequest("/posts", Method.GET);

            
            IRestResponse<List<Post>> response = client.Execute<List<Post>>(request);
            Console.WriteLine(response.Data.Count);

            
            for (int i=0; i<= response.Data.Count-1; i++)
            {
                Console.WriteLine(response.Data[i].Id);
            }

            //IRestResponse<List<Post>> posts = client.Get<List<Post>>(request);

            
            /*Post posts =
                new JsonDeserializer().
                Deserialize<Post>(response);*/
            
            // assert
            //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            //Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }
    }
}