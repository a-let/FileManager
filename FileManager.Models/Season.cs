using System.Collections.Generic;

namespace FileManager.Models
{
    public class Season : FileManagerObjectBase
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public int SeasonNumber { get; set; }
        public IEnumerable<Episode> EpisodeList { get; set; }
        public string Path { get; set; }
    }
}