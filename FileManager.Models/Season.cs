using FileManager.Models.Dtos;

using System.Collections.Generic;

namespace FileManager.Models
{
    public class Season
    {
        private int _seasonId;
        private int _showId;
        private int _seasonNumber;
        private string _path;

        public int SeasonId
        {
            get => _seasonId;
            set => _seasonId = value;
        }

        public int ShowId
        {
            get => _showId;
            set => _showId = value;
        }

        public int SeasonNumber
        {
            get => _seasonNumber;
            set => _seasonNumber = value;
        }

        public string Path
        {
            get => _path;
            set => _path = value;
        }

        private List<Episode> _episodes = new List<Episode>();
        public IEnumerable<Episode> Episodes { get => _episodes; }

        internal Season() { }

        public Season(SeasonDto seasonDto)
        {
            SeasonId = seasonDto.SeasonId;
            ShowId = seasonDto.ShowId;
            SeasonNumber = seasonDto.SeasonNumber;
            Path = seasonDto.Path;

            foreach (var episodeDto in seasonDto.Episodes)
            {
                var episode = new Episode(episodeDto);
                _episodes.Add(episode);
            }
        }

        public Season(int seasonId, int showId, int seasonNumber, string path, Episode[] episodes = null)
        {
            SeasonId = seasonId;
            ShowId = showId;
            SeasonNumber = seasonNumber;
            Path = path;

            if (episodes != null)
            {
                foreach (var episode in episodes)
                {
                    _episodes.Add(episode);
                }
            }
        }
    }
}