using System.IO;
using System.Reflection;

namespace Othello.Blazor.Server.Shared
{
    public class AppPath
    {
        public static string GetCurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string GetAiDirectory()
        {
            return $"{GetCurrentDirectory()}\\AI";
        }

        public static string GetAiInfosFilePath()
        {
            return $"{GetCurrentDirectory()}\\File\\AIInfos.json";
        }
    }
}
