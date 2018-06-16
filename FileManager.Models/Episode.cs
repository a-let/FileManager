using FileManager.Models.Interfaces;

namespace FileManager.Models
{
    public class Episode : FileManagerObjectBase, IVideo
    {
        public int EpisodeId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }

        private int _episodeNumber;
        public int EpisodeNumber
        {
            get => _episodeNumber;
            set
            {
                if (value != _episodeNumber)
                {
                    SetProperty("EpisodeNumber", ref _episodeNumber, value);
                    _episodeNumber = value;
                }
            }
        }

        private string _format;
        public string Format
        {
            get => _format;
            set
            {
                if(value != _format)
                {
                    SetProperty("Format", ref _format, value);
                    _format = value;                    
                }
            }
        }

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (value != _path)
                {
                    SetProperty("Path", ref _path, value);
                    _path = value;
                }
            }
        }
    }
}