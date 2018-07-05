namespace FileManager.Models
{
    public class Series : FileManagerObjectBase
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}