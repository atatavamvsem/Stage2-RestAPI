using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using RestAPITests.Resources;
using System.Net;
using RestSharp.Serialization.Json;
using RestAPITests.Utilities;

namespace Tests
{
    internal class BaseTest
    {

        [SetUp]
        public void Setup()
        {
            HTTPUtils.CreateClient();
        }
        
    }
}
