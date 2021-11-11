using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movie
{
    public class MovieSearchResult
    {
        public string imdb_id { get; set; };
        public string title { get; set; };
    }

    public class MovieSearchResults
    {
        public IList<MovieSearchResult> results { get; set; };
    }

    public class IMDBAPIClient
    {
        public async static MovieSearchResult getMovieIdByTitle(string rapidApiKey, string title)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("x-rapidapi-host", "data-imdb1.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", rapidApiKey);
            var uri = new Uri($"https://data-imdb1.p.rapidapi.com/movie/imdb_id/byTitle/{title}/");

            var response = await client.GetAsync(uri);

            JObject s = JObject.Parse(response);
            MovieSearchResults results = JsonConvert.DeserializeObject<Account>(s);
            Console.WriteLine(results.results[0].imdb_id);
        }

        public async static void setWebHook(string ngrokAddress, string BotToken)
        {
            var client = new HttpClient();
            var uri = new Uri("https://api.telegram.org/bot" + BotToken + "/setWebhook?url=" + ngrokAddress + "/api/BotController");

            await client.GetAsync(uri);
        }
    }
}