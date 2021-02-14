using System.IO;
using System.Text;
using System.Text.Json;

namespace Othello.Blazor.Server.Shared
{
    public class JsonFileIO
    {
         public static void WriteFile<T>(string filePath, Encoding encoding, T entity) 
        {
            var writeText = JsonSerializer.Serialize(entity);
            using var writer = new StreamWriter(filePath, false, encoding);
            writer.WriteLine(writeText);
        }

        public static T ReadAllLine<T>(string filePath, Encoding encoding)
        {
            using var sr = new StreamReader(filePath, encoding);
            var allLine = sr.ReadToEnd();
            return JsonSerializer.Deserialize<T>(allLine);
        }
    }
}
