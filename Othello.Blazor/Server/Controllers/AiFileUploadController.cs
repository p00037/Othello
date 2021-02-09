using Microsoft.AspNetCore.Mvc;
using Othello.Blazor.Server.Shared;
using Othello.Blazor.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Othello.Blazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AiFileUploadController : ControllerBase
    {
        // POST api/<AiFileUploadController>
        [HttpPost]
        public void Post(List<UploadFile> uploadFiles)
        {
            foreach (var uploadFile in uploadFiles)
            {
                WriteBinaryToFile($"{AppPath.AiDirectory()}\\{uploadFile.FileName}", uploadFile.Content);
            }
        }

        // バイナリデータをファイルに書き込み(書き込み先のフォルダがない場合は作成する)
        public static void WriteBinaryToFile(string path, byte[] data)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (var fs = new FileStream(path, FileMode.Create))
            using (var sw = new BinaryWriter(fs))
            {
                sw.Write(data);
            }
        }

        private string GetAppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
