using FileManager.Models;
using FileManager.Models.Constants;

using Swashbuckle.AspNetCore.Filters;

namespace FileManager.Web.SwaggerExamples
{
    public class MovieExample : IExamplesProvider<Movie>
    {
        public Movie GetExamples() => new Movie
        {
            MovieId = 0,
            SeriesId = 0,
            Category = "Test",
            Format = FileFormatTypes.MKV,
            IsSeries = true,
            Name = "Test Name",
            Path = @"C:\Temp\Test.mkv"
        };
    }
}