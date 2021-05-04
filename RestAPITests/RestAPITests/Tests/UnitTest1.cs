using Newtonsoft.Json;
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
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.ContentType.Contains("application/json"));

            List<Post> posts = response.Data;

            List<int> postsId = new List<int>();
            foreach (Post post in posts)
            {
                postsId.Add(post.Id);
            }
            List<int> originalPostsId = new List<int>(postsId);
            postsId.Sort();
            Assert.AreEqual(originalPostsId, postsId);
            //List<int> sortedPostsId = postsId.Sort();
            //IRestResponse<List<Post>> posts = client.Get<List<Post>>(request);


            /*Post posts =
                new JsonDeserializer().
                Deserialize<Post>(response);*/

            // assert
            //string s = response.ContentType.ToString();
            //Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [TestCase("posts", "99", 200, 10, 99)]
        [TestCase("posts", "150", 404, 0, 0)]
        public void GetPostTest(string resource, string number, int expectedHttpStatusCode, int userId, int id)
        {
            IRestClient client = new RestClient("https://jsonplaceholder.typicode.com");
            
            IRestRequest request = new RestRequest($"{resource}/{number}", Method.GET);

            IRestResponse<Post> response = client.Execute<Post>(request);
            Post posts = response.Data;
            Console.WriteLine(posts.UserId);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // OK
            }
            else
            {
                // NOK
            }
        }

        [TestCase("posts", 201, 1)]
        public void PostPostTest(string resource, int expectedHttpStatusCode, int userId)
        {
            Post post = new Post();
            post.Title = "dfd";
            post.UserId = userId;

            

            IRestClient client = new RestClient("https://jsonplaceholder.typicode.com");

            IRestRequest request = new RestRequest($"{resource}", Method.POST);

            request.AddHeader("Content-type", "application/json; charset=UTF-8");

            var jsons = JsonConvert.SerializeObject(post);
            //string json = JsonSerializer.Serialize(post);

            //string jsonData = "{ \"title\" : \"oo\",  \"body\" : \"bar\",  \"userId\" : 1  }";
            //new { body = "fff", userId = 1, title = "sdsdds" }
            request.AddJsonBody(jsons);

            IRestResponse<Post> response = client.Execute<Post>(request);
            Post posts = response.Data;
            //Post posts =
            //    new JsonDeserializer().
            //    Deserialize<Post>(response);
            Console.WriteLine(posts.Title);
            Assert.AreEqual(expectedHttpStatusCode, (int)response.StatusCode);
        }

        [TestCase("users", 200)]
        public void GetPostUser(string resource, int expectedHttpStatusCode)
        {
            IRestClient client = new RestClient("https://jsonplaceholder.typicode.com");

            IRestRequest request = new RestRequest($"{resource}", Method.GET);

            IRestResponse response = client.Execute(request);
            List<User> posts =
                new JsonDeserializer().
                Deserialize<List<User>>(response);
            //User posts = response.Data;
            Console.WriteLine(posts[1].Address.Geo.Lat);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // OK
            }
            else
            {
                // NOK
            }
        }
    }
}