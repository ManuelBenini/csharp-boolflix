public class MediaInfo
{
    public int Id { get; set; }
    public string Year { get; set; }
    public bool IsNew { get; set; }
    //public string VideoQuality { get; set; }


    //1 a 1 su media content (film) , un media content può essere un film
    public int? FilmId { get; set; }
    public Film? Film { get; set; }

    //1 a 1 su media content (serie) , un media content può essere una serie
    public int? TvSeriesId { get; set; }
    public TvSerie? TvSeries { get; set; }

    public List<Actor> Cast { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Feature> Features { get; set; }
}
