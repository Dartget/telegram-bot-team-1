using System.Threading;
using System.Threading.Tasks;

namespace telegram_bot.WebHookSetup
{
	public interface IWebHookClient
	{
		// funs for dev
		Task SendTextMessage(int chatId, string message);
	}
}
