using NUnit.Framework;
using RestAPITests.DataEntities;
using System;
using System.Collections.Generic;
using RestAPITests.Utilities;
using System.Resources;
using System.Net;

namespace Tests
{
    internal class ReasAPITests : BaseTest
    {
        readonly static ResourceManager testData = RestAPITests.Resources.testData.ResourceManager;

        //правильно ли выносить эту переменную сюда? где такие моменты должны быть?
        Post addedPost = new Post
        {
            Title = GenerateRandomInput.GenerateEnglishString(Int32.Parse(testData.GetString("lengthString"))),
            Body = GenerateRandomInput.GenerateEnglishString(Int32.Parse(testData.GetString("lengthString"))),
            UserId = Int32.Parse(testData.GetString("addPostUserId"))
        };

        [Test]
        public void RestAPITest()
        {
            //step1

            HTTPUtils.CreateGetPostsRequest();
            List<Post> listPosts = HTTPUtils.CreateResponse<List<Post>>();

            Assert.That(HTTPUtils.GetStatusCode(), Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(HTTPUtils.GetResponseContentType().Contains(testData.GetString("formatResponse")));
            Assert.IsTrue(Utils.CheckIdSorting(listPosts));

            //step2

            HTTPUtils.CreateGetPostsRequestWithParam(testData.GetString("existPost"));
            Post existPost = HTTPUtils.CreateResponse<Post>();

            Assert.That(HTTPUtils.GetStatusCode(), Is.EqualTo(HttpStatusCode.OK));
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Int32.Parse(testData.GetString("existPostUserId")), existPost.UserId);
                Assert.AreEqual(Int32.Parse(testData.GetString("existPostId")), existPost.Id);
                Assert.IsFalse(String.IsNullOrEmpty(existPost.Body));
                Assert.IsFalse(String.IsNullOrEmpty(existPost.Title));
            });

            //step3

            HTTPUtils.CreateGetPostsRequestWithParam(testData.GetString("notExistPost"));
            Post notExistPost = HTTPUtils.CreateResponse<Post>();

            Assert.That(HTTPUtils.GetStatusCode(), Is.EqualTo(HttpStatusCode.NotFound));
            Assert.AreEqual(HTTPUtils.GetResponseContent(), "{}");

            //step 4

            HTTPUtils.CreatePostPostsRequest();
            HTTPUtils.AddJsonBodyToRequest(addedPost);

            Post returnedPost = HTTPUtils.CreateResponse<Post>();

            Assert.That(HTTPUtils.GetStatusCode(), Is.EqualTo(HttpStatusCode.Created));
            Assert.Multiple(() =>
            {
                Assert.AreEqual(addedPost.UserId, returnedPost.UserId);
                Assert.AreEqual(addedPost.Body, returnedPost.Body);
                Assert.AreEqual(addedPost.Title, returnedPost.Title);
                Assert.IsTrue(returnedPost.Id > 0);
            });

            //step5
            
            User chekedUser = JsonConvertUtil.CreateFromJson<User>(testData.GetString("checkedUserPath"));

            HTTPUtils.CreateGetUsersRequest();
            List<User> listUsers = HTTPUtils.CreateResponse<List<User>>();

            Assert.That(HTTPUtils.GetStatusCode(), Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(HTTPUtils.GetResponseContentType().Contains(testData.GetString("formatResponse")));
            Assert.IsTrue(listUsers.Find(x => x.Id == Int32.Parse(testData.GetString("existUserId"))).Equals(chekedUser));

            //step6

            HTTPUtils.CreateGetUsersRequestWithParam(testData.GetString("existUser"));
            User existUser = HTTPUtils.CreateResponse<User>();

            Assert.That(HTTPUtils.GetStatusCode(), Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(existUser.Equals(chekedUser));
        }
    }
}