namespace csharp_boolflix.Models
{
    public class SupportClass
    {
        public Film? Film { get; set; }
        public TvSerie? TvSerie { get; set; }
        public int? tvSerieId { get; set; }
        public Episode? Episode { get; set; }
        public List<Actor>? Actors { get; set; }
        public List<int> SelectedActors { get; set; } = new();
        public List<Genre>? Genres { get; set; }
        public List<int> SelectedGenres { get; set; } = new();
        public List<Feature>? Features { get; set; }
        public List<int> SelectedFeatures { get; set; } = new();
    }
}
