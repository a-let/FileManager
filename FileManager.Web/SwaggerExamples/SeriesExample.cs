using FileManager.Models;

using Swashbuckle.AspNetCore.Filters;

namespace FileManager.Web.SwaggerExamples
{
    public class SeriesExample : IExamplesProvider<Series>
    {
        public Series GetExamples() => new Series
        {
            SeriesId = 0,
            Name = "Test",
            Path = @"C:\Test\Test_Series"
        };
    }
}