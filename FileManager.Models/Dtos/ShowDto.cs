using System.Collections.Generic;

namespace FileManager.Models.Dtos
{
    public sealed class ShowDto : Dto
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }

        public IEnumerable<SeasonDto> Seasons { get; set; }
    }
}
