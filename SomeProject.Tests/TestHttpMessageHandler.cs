using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SomeProject.Tests
{
    //Можно вызывать Verify на моке хэндлера вместо хака, но у Moq для защищенных методов менее удобный синтаксис
    //У хэндлера один метод Send(SendAsync), его проще верифицировать, чем сам HttpClient
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TestHttpMessageHandler: HttpMessageHandler
    {
        public virtual Task<HttpResponseMessage> SendTestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.SendAsync(request, cancellationToken);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.SendTestAsync(request, cancellationToken);
        }
    }
}