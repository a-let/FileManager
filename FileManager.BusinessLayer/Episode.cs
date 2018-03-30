using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                        Path = e.Path
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
            IEnumerable<Episode> episodes = new List<Episode>();

            using (var context = new FileManagerContext())
            {
                episodes = context.Episode.Where(e => e.SeasonId == id);
            }

            return episodes;
        }
    }
}