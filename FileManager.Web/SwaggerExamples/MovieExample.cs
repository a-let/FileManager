using FileManager.Models;
using FileManager.Models.Constants;

using Swashbuckle.AspNetCore.Filters;

using System.Diagnostics.CodeAnalysis;

namespace FileManager.Web.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
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