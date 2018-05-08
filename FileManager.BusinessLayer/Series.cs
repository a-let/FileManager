using System.Collections.Generic;

using FileManager.BusinessLayer.Interfaces;

namespace FileManager.BusinessLayer
{
    public class Series : FileManagerObjectBase, IFileManagerObject
    {
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        internal Series() { }

        public static Series NewSeries() => new Series();

        public void Save()
        {
            _commandText = "dbo.SeriesSave";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                _paramDict = new Dictionary<string, object>
                {
                    { "@SeriesId", this.SeriesId },
                    { "@MovieName", this.Name },
                    { "@Path", this.Path }
                };

                _fileManagerDb.AddParameters(_paramDict);

                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Series> GetSeries()
        {
            var series = new List<Series>();

            _commandText = "dbo.SeriesGetList";
            using (var connection = _fileManagerDb.CreateConnection())
            using (var command = _fileManagerDb.CreateCommand(_commandText))
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    series.Add(new Series
                    {
                        SeriesId = (int)reader["SeriesId"],
                        Name = (string)reader["SeriesName"],
                        Path = (string)reader["FilePath"]
                    });
                }               
            }

            return series;
        }

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