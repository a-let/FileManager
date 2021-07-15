using FileManager.Models.Dtos;

using System.Collections.Generic;

namespace FileManager.Models
{
    public class Show 
    {
        private int _showId;
        private string _name;
        private string _category;
        private string _path;

        public int ShowId
        {
            get => _showId;
            private set => _showId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Category
        {
            get => _category;
            set => _category = value;
        }

        public string Path
        {
            get => _path;
            private set => _path = value;
        }

        public bool UpdatePath(string path)
        {
            var isPathValid = true; // TODO : Validation rule? Return error message?

            if (isPathValid)
                Path = path;

            return isPathValid;
        }

        private List<Season> _seasons = new List<Season>();
        public IEnumerable<Season> Seasons { get => _seasons; }

        public void AddSeason(Season season)
        {
            if (season != null)
                _seasons.Add(season);
        }

        public void RemoveSeason(Season season)
        {
            if (season != null)
                _seasons.Remove(season);
        }

        internal Show() { }

        public Show(ShowDto showDto)
        {
            ShowId = showDto.ShowId;
            Name = showDto.Name;
            Category = showDto.Category;
            Path = showDto.Path;

            // TODO
            foreach (var seasonDto in showDto.Seasons)
            {
                var season = new Season(seasonDto);
                _seasons.Add(season);
            }
        }

        // TODO : Should id be passed in?
        public Show(int showId, string name, string category, string path, Season[] seasons = null)
        {
            // TODO : Should showId be a param here?
            ShowId = showId;
            Name = name;
            Category = category;
            Path = path;

            if (seasons != null)
            {
                foreach (var season in seasons)
                    _seasons.Add(season);
            }
        }
    }
}