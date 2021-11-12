using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using telegram_bot.Services;
using telegram_bot.Types;

namespace telegram_bot.Controllers
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
