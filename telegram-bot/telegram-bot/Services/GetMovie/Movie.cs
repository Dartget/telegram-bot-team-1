using System.Collections.Generic;

namespace TelegramBot.Services.GetMovie
{
    public class MovieResult
    {
        public string ImdbId { get; set; }
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

    public class Movie
    {
        public string Description { get; set; }
        public string Popularity { get; set; }
        public string Year { get; set; }
        public string MovieLength { get; set; }
        public string ImageUrl { get; set; }
        public IList<MovieGenre> Gen { get; set; }
        public IList<MovieKeyword> Keywords { get; set; }
    }
}
