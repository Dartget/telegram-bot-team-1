using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot.WebHookSetup
{
	public interface ISetupClient
	{
		Task SetWebhookAsync(string url, CancellationToken cancellationToken);
		Task DeleteWebhookAsync(string url, CancellationToken cancellationToken);
	}
}
