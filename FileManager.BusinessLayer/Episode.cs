using System.Collections.Generic;
using System.Linq;
using FileManager.BusinessLayer;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Episode : IFileManagerObject, IVideo
    {
        public int EpisodeId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public string EpisodeNumber { get; set; }
        public string Format { get; set; }
        public string Path { get; set; }
                
        private Episode() { }

        public static Episode NewEpisode() => new Episode();

        public async void Save()
        {
            using (var context = new FileManagerContext())
            {
                context.Episode.Add(this);
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
                        Path = e.Path
                    });
                }
            }

            return episodes;
        }
                
        public static Episode FindEpisode(string name)
        {
            var episode = new Episode();

            using (var context = new FileManagerContext())
            {
                episode = context.Episode
                    .SingleOrDefault(e => e.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            }

            return episode;
        }
        
        // TODO: Implement, need to be internal?
        internal static List<Episode> GetEpisodesBySeasonId()
        {
            return null;
        }
    }
}