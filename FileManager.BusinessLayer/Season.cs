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
        public IEnumerable<Episode> EpisodeList { get; set; }
        public string Path { get; set; }

        internal Season() { }

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
                        EpisodeList = Episode.GetEpisodesBySeasonId(s.SeasonId),
                        Path = s.Path
                    });
                }
            }

            return seasons;
        }

        public static Season GetSeason(int seasonId)
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