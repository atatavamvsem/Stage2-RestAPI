using Newtonsoft.Json;
using NUnit.Framework;
using RestAPITests.DataEntities;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Configuration;
using System.Reflection;
using RestAPITests.Resources;
using RestAPITests.Utilities;

namespace Tests
{
    internal class ReasAPITests : BaseTest
    {

        [Test]
        public void Test2()
        {
            //step1
            IRestClient client = new RestClient(Resources.URL);
            IRestRequest request = new RestRequest(Resources.resourcePosts, Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(response.ContentType.Contains(Resources.formatResponse));

            List<Post> listPosts = new JsonDeserializer().Deserialize<List<Post>>(response);
            
            Assert.IsTrue(Utils.CheckIdSorting(listPosts));

            //step2
            request = new RestRequest($"{Resources.resourcePosts}/{Resources.existPost}", Method.GET);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Post post = new JsonDeserializer().Deserialize<Post>(response);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(Resources.existPostUserId, post.UserId);
                Assert.AreEqual(Resources.existPostId, post.Id);
                Assert.IsFalse(String.IsNullOrEmpty(post.Body));
                Assert.IsFalse(String.IsNullOrEmpty(post.Title));
            });

            //step3
            request = new RestRequest($"{Resources.resourcePosts}/{Resources.notExistPost}", Method.GET);
            response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.AreEqual(response.Content, "{}");
            });
            //step 4
            Post addedPost = new Post
            {
                Title = GenerateRandomInput.GenerateEnglishString(Resources.lengthString),
                Body = GenerateRandomInput.GenerateEnglishString(Resources.lengthString),
                UserId = Resources.addPostUserId
            };

            request = new RestRequest(Resources.resourcePosts, Method.POST);

            //request.AddHeader("Content-type", "application/json; charset=UTF-8");
            request.AddJsonBody(JsonConvert.SerializeObject(addedPost));

            response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            Post returnedPost = new JsonDeserializer().Deserialize<Post>(response);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(addedPost.UserId, returnedPost.UserId);
                Assert.AreEqual(addedPost.Body, returnedPost.Body);
                Assert.AreEqual(addedPost.Title, returnedPost.Title);
                Assert.IsTrue(returnedPost.Id > 0);
            });

            //step5
            

            request = new RestRequest(Resources.resourceUsers, Method.GET);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(response.ContentType.Contains(Resources.formatResponse));

            //jsc
            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\Resources\jsconfig1.json");
            String jsonString = File.ReadAllText(@"..\..\..\Resources\jsconfig1.json");
            User user = JsonConvert.DeserializeObject<User>(jsonString);

            List<User> posts = new JsonDeserializer().Deserialize<List<User>>(response);

            Assert.IsTrue(posts.Find(x => x.Id == 5).Equals(user));

            //step6
            request = new RestRequest($"{Resources.resourceUsers}/{Resources.existUser}", Method.GET);
            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            User userFive = new JsonDeserializer().Deserialize<User>(response);
            Assert.IsTrue(userFive.Equals(user));
        }


        
    }
}