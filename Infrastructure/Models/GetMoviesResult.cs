using Newtonsoft.Json;

namespace Infrastructure.Models
{
    internal class GetMoviesResult
    {
        public IEnumerable<GetMoviesItemResult> Results { get; set; }
    }

    internal class GetMoviesItemResult
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Overview { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public DateTimeOffset? ReleaseDate { get; set; }
    }
}
