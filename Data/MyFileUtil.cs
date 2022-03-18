using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Myfirst.Data
{
    public static class MyFileUtil
    {
        public static async Task MyCreateFileAsync<T>(List<T> TList, string pathAddress, string fileName)
        {
            var json = JsonConvert.SerializeObject(TList);
            await File.WriteAllTextAsync(@$"{pathAddress}\{fileName}", json);
        }

        public static async Task MyCreateFileAsync<T>(T TObject, string pathAddress, string fileName)
        {
            var json = JsonConvert.SerializeObject(TObject);
            await File.WriteAllTextAsync(@$"{pathAddress}\{fileName}", json);
        }

        public static T MyReadFile<T>(string pathAddress, string fileName)
        {
            var json = File.ReadAllText(Path.Combine(pathAddress, fileName));

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
