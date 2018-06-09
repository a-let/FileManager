using System.Collections.Generic;

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

        //public void Save()
        //{
        //    _commandText = "dbo.ShowSave";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        command.Parameters.AddWithValue("@ShowId", this.ShowId);
        //        command.Parameters.AddWithValue("@Name", this.Name);
        //        command.Parameters.AddWithValue("@Category", this.Category);
        //        command.Parameters.AddWithValue("@Path", this.Path);

        //        command.ExecuteNonQuery();
        //    }
        //}

        //public static IEnumerable<Show> GetShows()
        //{
        //    var shows = new List<Show>();

        //    _commandText = "dbo.ShowGetList";
        //    var fileManagerDb = GetDb();
        //    using (var connection = fileManagerDb.CreateConnection())
        //    using (var command = fileManagerDb.CreateCommand())
        //    {
        //        connection.Open();
        //        var reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            shows.Add(new Show
        //            {
        //                ShowId = (int)reader["ShowId"],
        //                Name = (string)reader["ShowName"],
        //                Category = (string)reader["ShowCategory"],
        //                Path = (string)reader["FilePath"]
        //            });
        //        }
        //    }

        //    return shows;
        //}

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