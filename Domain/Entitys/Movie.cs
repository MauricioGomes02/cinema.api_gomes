namespace Domain.Entitys
{
    public class Movie
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? ReleaseDate { get; set; }
    }
}
