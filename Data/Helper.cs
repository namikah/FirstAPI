using System.Collections.Generic;
using System.Reflection;

namespace Myfirst.Data
{
    public static class Helper
    {
        public static IEnumerable<string> GetAllClassPropertiesName(string className)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (var item in assembly.GetTypes())
            {
                if (item.Name.ToLower() != className.ToLower())
                    continue;

                    foreach (var property in item.GetProperties())
                    {
                        yield return property.Name;
                    }
            }
        }
    }
}
