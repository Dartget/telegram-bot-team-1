using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramBot.Services;
using TelegramBot.Types;

namespace TelegramBot.Controllers
{
    [Route("api/BotController")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService, [FromBody] Update update)
        {
			if (update.Message != null)
				await handleUpdateService.HandlerAsync(update);
			return Ok();
        }
    }
}
