using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot.WebHookSetup
{
    public sealed class SetupClient : ISetupClient
    {
		private readonly HttpClient _client;
		public SetupClient(HttpClient httpClient)
		{
			_client = httpClient;
		}

		public async Task SetWebhookAsync(string url, CancellationToken cancellationToken)
		{
			await _client.GetAsync(url, cancellationToken);
		}
		public async Task DeleteWebhookAsync(string url, CancellationToken cancellationToken)
		{
			await _client.GetAsync(url, cancellationToken);
		}
	}
}
