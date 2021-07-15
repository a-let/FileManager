using FileManager.Models.Dtos;

namespace FileManager.Models
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public string Format { get; set; }
        public string Path { get; set; }

        internal Episode() { }

        public Episode(EpisodeDto episodeDto)
        {
            EpisodeId = episodeDto.EpisodeId;
            SeasonId = episodeDto.SeasonId;
            Name = episodeDto.Name;
            EpisodeNumber = episodeDto.EpisodeNumber;
            Format = episodeDto.Format;
            Path = episodeDto.Path;
        }

        public Episode(int episodeId, int seasonId, string name, int episodeNumber, string format, string path)
        {
            EpisodeId = episodeId;
            SeasonId = seasonId;
            Name = name;
            EpisodeNumber = episodeNumber;
            Format = format;
            Path = path;
        }
    }
}