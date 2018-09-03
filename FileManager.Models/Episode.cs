namespace FileManager.Models
{
    public class Episode
    {
        private Episode episode;

        public int EpisodeId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public string Format { get; set; }
        public string Path { get; set; }
    }
}