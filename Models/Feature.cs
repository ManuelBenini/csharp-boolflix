using csharp_boolflix.Models;

public class Feature
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<MediaInfo> MediaInfos { get; set; }
}