using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace telegram_bot
{
    public class MovieResult
    {
        public string ImdbId { get; set; };
        public string Title { get; set; };
    }
}
