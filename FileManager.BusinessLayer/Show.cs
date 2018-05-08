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
            _commandText = "dbo.ShowSave";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                _paramDict = new Dictionary<string, object>
                {
                    { "@ShowId", this.ShowId },
                    { "@Name", this.Name },
                    { "@Category", this.Category },
                    { "@Path", this.Path }
                };

                _fileManagerDb.AddParameters(_paramDict);

                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Show> GetShows()
        {
            var shows = new List<Show>();

            _commandText = "dbo.ShowGetList";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
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