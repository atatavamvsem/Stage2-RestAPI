using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using RestAPITests.Resources;

namespace Tests
{
    internal class BaseTest
    {
        IRestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(Resources.URL);
        }
    }
}
