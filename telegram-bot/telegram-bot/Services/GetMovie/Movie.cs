using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace TelegramBot.Services.GetMovie
{
	[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
	public class MovieResult
    {
		[JsonProperty(Required = Required.Always)]
		public string ImdbId { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string Title { get; set; }
    }
	
	public class MovieGenre
    {
        public int Id { get; set; }
        public string Genre { get; set; }
    }

    public class MovieKeyword
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
    }
	[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
	public class Movie
    {
		[JsonProperty(Required = Required.Always)]
		public string Description { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string Popularity { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string Year { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string MovieLength { get; set; }
		[JsonProperty(Required = Required.Always)]
		public string ImageUrl { get; set; }
		[JsonProperty(Required = Required.Always)]
		public IList<MovieGenre> Gen { get; set; }
		[JsonProperty(Required = Required.Always)]
		public IList<MovieKeyword> Keywords { get; set; }
    }
}
