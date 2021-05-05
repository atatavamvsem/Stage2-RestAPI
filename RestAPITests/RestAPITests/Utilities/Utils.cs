using RestAPITests.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPITests.Utilities
{
    class Utils
    {
        public static bool CheckIdSorting(List<Post> listPosts)
        {
            int firstId = listPosts[0].Id;
            foreach (Post post in listPosts)
            {
                if (firstId > post.Id)
                {
                    return false;
                }
                firstId = post.Id;
            }
            return true;
        }
    }
}
