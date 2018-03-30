using System.Collections.Generic;
using System.Data.Entity;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Season : IFileManagerObject
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public string SeasonNumber { get; set; }
        public IEnumerable<Episode> Episodes { get; set; }
        public string Path { get; set; }

        private Season() { }

        public static Season NewSeason()
        {
            using (var context = new FileManagerContext())
            {
                var season = new Season();
                season = context.Season.Create();
                return season;
            }                 
        }

        public async void SaveAsync()
        {
            using (var context = new FileManagerContext())
            {
                context.Season.Add(this);

                context.Entry(this).State = this.SeasonId == 0 ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        // TODO: Episodes to EpisodeList? Fix Episode.Get call
        public static IEnumerable<Season> GetSeasons()
        {
            var seasons = new List<Season>();

            using (var context = new FileManagerContext())
            {
                foreach (var s in context.Season)
                {
                    seasons.Add(new Season()
                    {
                        SeasonId = s.SeasonId,
                        ShowId = s.ShowId,
                        SeasonNumber = s.SeasonNumber,
                        Episodes = Episode.GetEpisodesBySeasonId(0),
                        Path = s.Path
                    });
                }
            }

            return seasons;
        }

        public static Season FindSeason(int seasonId)
        {
            var season = new Season();

            using (var context = new FileManagerContext())
            {
                season = context.Season.Find(seasonId);
            }

            return season;
        }
    }
}