using System.Collections.Generic;
using System.Linq;
using FileManager.BusinessLayer;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Series : IFileManagerObject
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }

        private Series() { }

        public static Series NewSeries() => new Series();

        public async void Save()
        {
            using (var context = new FileManagerContext())
            {
                context.Series.Add(this);
                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<Series> GetSeries()
        {
            var series = new List<Series>();

            using (var context = new FileManagerContext())
            {
                foreach (var s in context.Series)
                {
                    series.Add(new Series()
                    {
                        SeriesId = s.SeriesId,
                        Name = s.Name
                    });
                }
            }
                return series;
        }

        public static Series FindSeries(string name)
        {
            var series = new Series();

            using (var context = new FileManagerContext())
            {
                series = context.Series
                    .SingleOrDefault(s => s.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            }

            return series;
        }
    }
}