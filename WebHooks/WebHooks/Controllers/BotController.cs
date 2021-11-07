using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebHooks.Controllers
{
    [Route("api/BotController")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private const string token = @"2094560790:AAGcEq2UvuXGNCtNtdV6C-t3b6-y6W-X9AM";
        private const string tgUrl = "https://api.telegram.org/bot";

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var client = new HttpClient();
            var uri = new Uri(tgUrl + token + "/sendMessage");

            string messageToReturn =
                "hello, world!\n\n" +
                "this message was sent using web hooks.\n" +
                 "update_id: " + update.update_id + "\n" +
                 "message_id: " + update.message.message_id + "\n\n" +
                 "info about user:\n" +
                 "user id: " + update.message.from.id + "\n" +
                 "user is bot: " + update.message.from.is_bot + "\n" +
                 "user first name: " + update.message.from.first_name + "\n" +
                 "user nickname: " + update.message.from.username + "\n" +
                 "user language code: " + update.message.from.language_code + "\n\n" +
                 "info about chat:\n" +
                 "chat id: " + update.message.chat.id + "\n" +
                 "chat first name: " + update.message.chat.first_name + "\n" +
                 "chat nickname: " + update.message.chat.username + "\n" +
                 "chat type: " + update.message.chat.type + "\n\n" +
                 "unix date: " + update.message.date + "\n\n" +
                 "text message: " + update.message.text + "\n\n" +
                 "have a nice day.";

            var Params = new Dictionary<string, string>()
            {
                { "chat_id", update.message.from.id.ToString() },
                { "text", messageToReturn }
            };


            string json = JsonConvert.SerializeObject(Params);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(uri, requestData);

            return Ok();
        }
    }
}
