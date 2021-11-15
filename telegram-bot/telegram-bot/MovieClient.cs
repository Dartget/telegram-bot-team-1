using System;
using Movie;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        ImdbApiClient(string rapidApiKey, string imdbApiUrl) {
            RapidApiKey = rapidApiKey;
            ImdbApiUrl = imdbApiUrl;
        }

        public async MovieSearchResults getMovieIdByTitle(string title)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("x-rapidapi-host", "data-imdb1.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("x-rapidapi-key", RapidApiKey);

            var uri = new Uri($"{ImdbApiUrl}{title}/");
            var response = await client.GetAsync(uri);

            JObject jsonResponse = JObject.Parse(response);
            MovieSearchResults deserializedJsonResponse = JsonConvert.DeserializeObject<MovieSearchResults>(jsonResponse);
            if (deserializedJsonResponse != null) {
                Console.WriteLine(deserializedJsonResponse.results[0].imdb_id);
                return deserializedJsonResponse;
            } else {
                return new MovieSearchResult();
            }
        }
    }
}