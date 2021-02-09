using System.IO;
using System.Reflection;

namespace Othello.Blazor.Server.Shared
{
    public class AppPath
    {
        public static string CurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string AiDirectory()
        {
            return $"{CurrentDirectory()}\\AI";
        }
    }
}
