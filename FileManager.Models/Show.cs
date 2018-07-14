namespace FileManager.Models
{
    public class Show : FileManagerObjectBase
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }
    }
}