using System.Collections.Generic;

namespace FileManager.BusinessLayer
{
    public class Season : FileManagerObjectBase
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public int SeasonNumber { get; set; }
        public IEnumerable<Episode> EpisodeList { get; set; }
        public string Path { get; set; }

        internal Season() { }

        public static Season NewSeason() => new Season();

        //public static Season GetSeason(int seasonId)
        //{
        //    var season = new Season();

        //    using (var context = new FileManagerContext())
        //    {
        //        season = context.Season.Find(seasonId);
        //    }

        //    return season;
        //}
    }
}