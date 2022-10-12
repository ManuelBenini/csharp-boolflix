using csharp_boolflix.Models;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Fullname { get { return Name + " " + Surname; } }


    public List<MediaInfo>? MediaInfos { get; set; }
}
