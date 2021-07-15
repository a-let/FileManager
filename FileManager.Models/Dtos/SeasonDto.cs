using System.Collections.Generic;

namespace FileManager.Models.Dtos
{
    public sealed class SeasonDto : Dto
    {
        public int SeasonId { get; set; }
        public int ShowId { get; set; }
        public int SeasonNumber { get; set; }
        public string Path { get; set; }

        public IEnumerable<EpisodeDto> Episodes { get; set; }
    }
}
