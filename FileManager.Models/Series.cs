namespace FileManager.Models
{
    public class Series : FileManagerObjectBase
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        //public static Series GetSeries(string name)
        //{
        //    var series = new Series();

        //    using (var context = new FileManagerContext())
        //    {
        //        series = context.Series
        //            .SingleOrDefault(s => s.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        //    }

        //    return series;
        //}
    }
}