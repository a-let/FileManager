using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Show : FileManagerObjectBase, IFileManagerObject, IVideo
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }

        internal Show() { }

        public static Show NewShow() => new Show();

        public void Save()
        {
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = new SqlCommand("dbo.ShowSave", connection) { CommandType = CommandType.StoredProcedure })
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@ShowId", this.ShowId));
                command.Parameters.Add(new SqlParameter("@Name", this.Name));
                command.Parameters.Add(new SqlParameter("@Category", this.Category));
                command.Parameters.Add(new SqlParameter("@Path", this.Path));
                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Show> GetShows()
        {
            var shows = new List<Show>();

            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = new SqlCommand("dbo.ShowGetList", connection) { CommandType = CommandType.StoredProcedure })
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    shows.Add(new Show
                    {
                        ShowId = (int)reader["ShowId"],
                        Name = (string)reader["ShowName"],
                        Category = (string)reader["ShowCategory"],
                        Path = (string)reader["FilePath"]
                    });
                }
            }

            return shows;
        }

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