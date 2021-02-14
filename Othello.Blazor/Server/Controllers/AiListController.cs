using Microsoft.AspNetCore.Mvc;
using Othello.Blazor.Shared;
using System.Collections.Generic;

namespace Othello.Blazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AiListController : ControllerBase
    {
        [HttpGet]
        public void Get(List<AiInfo> value)
        {

        }
    }
}
