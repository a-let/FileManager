using System.Collections.Generic;
using FileManager.BusinessLayer;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Season : IFileManagerObject
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public string SeasonNumber { get; set; }
        public List<Episode> Episodes { get; set; }
        public string Path { get; set; }

        private Season() { }

        public static Season NewSeason() => new Season();

        public async void Save()
        {
            using (var context = new FileManagerContext())
            {
                context.Season.Add(this);
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
                        Episodes = Episode.GetEpisodesBySeasonId(),
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