using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Show : FileManagerObjectBase, IVideo
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }

        internal Show() { }

        public static Show NewShow() => new Show();

        //public static Show GetShow(string name)
        //{
        //    var show = new Show();

        //    using (var context = new FileManagerContext())
        //    {
        //        show = context.Show
        //            .SingleOrDefault(s => s.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        //    }

        //    return show;
        //}
    }
}