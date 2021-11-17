using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SomeProject.Services
{
    public class ClientSettings
    {
        public string ApiUrl { get; init; }
        public string TimeZone { get; init; }
    }
    public class TimeResponse
    {
        public string Date { get; set; }
    }
    
    public class SomeClient
    {
        private readonly HttpClient httpClient;
        private readonly ClientSettings settings;

        public SomeClient(HttpClient httpClient, ClientSettings settings)
        {
            this.httpClient = httpClient;
            this.settings = settings;
        }

        public async Task<string> GetSomeData()
        {
            var url = $"{this.settings.ApiUrl.TrimEnd('/')}/timezone/{this.settings.TimeZone.TrimStart('/')}";
            var response = await this.httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonTimeResponse = await response.Content.ReadAsStringAsync();
            var timeResponse = System.Text.Json.JsonSerializer.Deserialize<TimeResponse>(jsonTimeResponse);
            return timeResponse?.Date;
        }
    }
}