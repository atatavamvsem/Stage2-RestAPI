using Newtonsoft.Json;
using System.IO;

namespace RestAPITests.Utilities
{
    class JsonConvertUtil
    {
        public static T CreateFromJson<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static string SerializeObj<T>(T addedPost)
        {
            return JsonConvert.SerializeObject(addedPost);
        }
    }
}
