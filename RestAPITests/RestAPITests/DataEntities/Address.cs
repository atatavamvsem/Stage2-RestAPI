using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPITests.DataEntities
{
    class Address
    {
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("suite")]
        public string Suite { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
        [JsonProperty("geo")]
        public Geo Geo { get; set; }

        public override bool Equals(object obj)
        {
            var address = obj as Address;
            return address != null &&
                   Street == address.Street &&
                   Suite == address.Suite &&
                   City == address.City &&
                   Zipcode == address.Zipcode &&
                   Geo.Equals(address.Geo);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, Suite, City, Zipcode, Geo);
        }
    }

}
