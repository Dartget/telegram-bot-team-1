using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TelegramBot.Services.GetMovieService
{
    public class MovieSearchResult
    {
        public string imdb_id { get; set; }
        public string title { get; set; }
    }

    public class MovieSearchResults
    {
        public IList<MovieSearchResult> results { get; set; }
    }
}
