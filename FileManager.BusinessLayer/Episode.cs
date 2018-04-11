using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Episode : FileManagerObjectBase, IFileManagerObject, IVideo
    {
        public int EpisodeId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }

        private string _episodeNumber;
        public string EpisodeNumber
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
                
        internal Episode() { }

        public static Episode NewEpisode()
        {
            using (var context = new FileManagerContext())
            {
                var episode = new Episode();
                episode = context.Episode.Create();
                return episode;
            }
        }

        public async void SaveAsync()
        {
            using (var context = new FileManagerContext())
            {
                context.Episode.Add(this);

                context.Entry(this).State = this.EpisodeId == 0 ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<Episode> GetEpisodes()
        {
            var episodes = new List<Episode>();

            using (var context = new FileManagerContext())
            {
                foreach (var e in context.Episode)
                {
                    episodes.Add(new Episode()
                    {
                        EpisodeId = e.EpisodeId,
                        SeasonId = e.SeasonId,
                        Name = e.Name,
                        EpisodeNumber = e.EpisodeNumber,
                        Format = e.Format,
                        Path = e.Path,
                        IsChanged = false
                    });
                }
            }

            return episodes;
        }
               
        public static Episode GetEpisode(string name)
        {
            var episode = new Episode();

            using (var context = new FileManagerContext())
            {
                episode = context.Episode
                    .SingleOrDefault(e => e.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            }

            return episode;
        }
        
        public static IEnumerable<Episode> GetEpisodesBySeasonId(int id)
        {
            var episodes = new List<Episode>();

            using (var context = new FileManagerContext())
            {
                foreach(var e in context.Episode.Where(e => e.SeasonId == id))
                {
                    episodes.Add(new Episode()
                    {
                        EpisodeId = e.EpisodeId,
                        SeasonId = e.SeasonId,
                        Name = e.Name,
                        EpisodeNumber = e.EpisodeNumber,
                        Format = e.Format,
                        Path = e.Path
                    });
                }
            }

            return episodes;
        }
    }
}