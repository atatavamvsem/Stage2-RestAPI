using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPITests.Resources
{
    class Resources
    {
        public const string resourcePosts = "posts";
        public const string resourceUsers = "users";

        public const string existPost = "99";
        public const int existPostUserId = 10;
        public const int existPostId = 99;

        public const string notExistPost = "150";
        
        public const int addPostUserId = 1;

        public const string existUser = "5";
        public const int existUserId = 5;

        public const string URL = "https://jsonplaceholder.typicode.com";
        public const string formatResponse = "application/json";
        public const int lengthString = 10;
    }
}
