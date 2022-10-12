using csharp_boolflix.Models;

public class Episode : MediaContent
{
    public int SeasonNumber { get; set; }

    public int TvSeriesId { get; set; }
    public TvSerie TvSeries { get; set; }
}
