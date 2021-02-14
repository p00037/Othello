using Microsoft.AspNetCore.Mvc;
using Othello.Blazor.Server.Repository;
using Othello.Blazor.Server.Shared;
using Othello.Blazor.Shared;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Othello.Blazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AiFileUploadController : ControllerBase
    {
        [HttpPost]
        public void Post(UploadFile uploadFile)
        {
            //foreach (var uploadFile in uploadFiles)
            //{
                WriteBinaryToFile($"{AppPath.GetAiDirectory()}\\{uploadFile.FileName}", uploadFile.Content);
            var aiInfo = new AiInfo() { DisplayName = uploadFile.DisplayName, FileName = uploadFile.FileName };
            //var aiInfos = new List<AiInfo>() { aiInfo };
            //JsonFileIO.WriteFile(AppPath.GetAiInfosFilePath(), Encoding.UTF8, aiInfos);
            var aiInfoRepository = new AiInfoRepository();
            aiInfoRepository.Save(aiInfo);
            //}
        }

        // バイナリデータをファイルに書き込み(書き込み先のフォルダがない場合は作成する)
        public void WriteBinaryToFile(string path, byte[] data)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using var fs = new FileStream(path, FileMode.Create);
            using var sw = new BinaryWriter(fs);
            sw.Write(data);
        }


    }
}
