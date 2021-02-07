using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Othello.Blazor.Shared;
using Othello.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Othello.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AiController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public AiController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public BoardPoint Post(AiConfig aiConfig)
        {
            IAi ai = CreateAiObject(aiConfig.TurnPiece,aiConfig.AiName);
            var board = aiConfig.GetBoard();
            return ai.HitPieceBoardPoint(board);
        }

        private IAi CreateAiObject(Piece turnPiece, string AiName)
        {
            // DLLをAssemblyにロードする
            var asm = Assembly.LoadFrom(GetAppPath() + @$"\AI\{AiName}.dll");

            // クラスをインスタンス化
            return (IAi)asm.CreateInstance("sampleAI.AI",
                false,
                BindingFlags.CreateInstance,
                null,
                new object[] { turnPiece },
                null,
                null);
        }

        private string GetAppPath()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
