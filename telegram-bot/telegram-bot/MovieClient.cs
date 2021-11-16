using System;
using Movie;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace telegram_bot
{
    interface IImdbApiClient
    {
        MovieSearchResult getMovieIdByTitle(string title);
    }

    public class ImdbApiClient : IImdbApiClient
    {
        public string RapidApiKey { get; }
        public string ImdbApiUrl { get; }
		private HttpClient client;

        ImdbApiClient(string rapidApiKey, string imdbApiUrl) {
            RapidApiKey = rapidApiKey;
            ImdbApiUrl = imdbApiUrl;
            client = new HttpClient();
        }

        public async IList<MovieResult> getMovieIdByTitle(string title)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{ImdbApiUrl}{title}/"),
                Headers =
                {
                    { "x-rapidapi-host", "data-imdb1.p.rapidapi.com" },
                    { "x-rapidapi-key", RapidApiKey },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                JObject movieSearch = JObject.Parse(await response.Content.ReadAsStringAsync());
                JEnumerable<JToken> results = movieSearch["results"].Children();
                IList<MovieResult> movieResults = new List<MovieResult>();

                foreach (JToken result in results)
                {
                    MovieResult movieResult = result.ToObject<MovieResult>();
                    movieResults.Add(movieResult);
                }

                return movieResults;
            }
        }
    }
}
