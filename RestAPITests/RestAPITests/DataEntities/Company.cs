using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPITests.DataEntities
{
    class Company
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("catchPhrase")]
        public string CatchPhrase { get; set; }
        [JsonProperty("bs")]
        public string Bs { get; set; }

        public override bool Equals(object obj)
        {
            var company = obj as Company;
            return company != null &&
                   Name == company.Name &&
                   CatchPhrase == company.CatchPhrase &&
                   Bs == company.Bs;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CatchPhrase, Bs);
        }
    }
}
