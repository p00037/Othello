using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Othello.Blazor.Server.Shared;
using Othello.Blazor.Shared;
using Othello.Shared;
using System.Reflection;

namespace Othello.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AiController : ControllerBase
    {
        private readonly ILogger<AiController> _logger;

        public AiController(ILogger<AiController> logger)
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

        private IAi CreateAiObject(Piece turnPiece, string aiName)
        {
            // DLLをAssemblyにロードする
            var asm = Assembly.LoadFrom($"{AppPath.GetAiDirectory()}\\{aiName}");

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
