using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace telegram_bot.WebHookSetup
{
    public sealed class Singleton
    {
		private static Singleton _instance;
		private readonly HttpClient _client = new();
		private static Singleton Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Singleton();

				return _instance;
			}
		}

		public async Task SetWebhookAsync(string url, CancellationToken cancellationToken)
		{
			await Instance._client.GetAsync(url, cancellationToken);
		}
		public async Task DeleteWebhookAsync(string url, CancellationToken cancellationToken)
		{
			await Instance._client.GetAsync(url, cancellationToken);
		}
	}
}
