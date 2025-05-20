using System;

namespace Filme.API.Dtos
{
    public class MovieGetResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
    }
}
