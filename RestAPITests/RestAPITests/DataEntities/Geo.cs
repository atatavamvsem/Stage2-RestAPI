using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPITests.DataEntities
{
    class Geo
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }
        [JsonProperty("lng")]
        public string Lng { get; set; }
    }
}
