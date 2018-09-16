using System.Collections.Generic;

namespace FileManager.Models
{
    public class Show 
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
        public string Path { get; set; }
    }
}