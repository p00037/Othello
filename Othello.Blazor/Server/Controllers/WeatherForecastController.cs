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
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var boardstate = new BoardState();
            IAi ai = CreateAiObject(Piece.Black);
            var point = ai.HitPieceBoardPoint(boardstate.Board);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private IAi CreateAiObject(Piece piece)
        {
            // DLLをAssemblyにロードする
            var asm = Assembly.LoadFrom(GetAppPath() + @"\AI\sampleAI.dll");

            // クラスをインスタンス化
            return (IAi)asm.CreateInstance("sampleAI.AI",
                false,
                BindingFlags.CreateInstance,
                null,
                new object[] { piece },
                null,
                null);
        }

        private string GetAppPath()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
