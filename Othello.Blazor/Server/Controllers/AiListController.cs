using Microsoft.AspNetCore.Mvc;
using Othello.Blazor.Server.Repository;
using Othello.Blazor.Shared;
using System.Collections.Generic;

namespace Othello.Blazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AiListController : ControllerBase
    {
        [HttpGet]
        public List<AiInfo> Get()
        {
            var aiInfoRepository = new AiInfoRepository();
            return aiInfoRepository.GetEntitys();
        }
    }
}
