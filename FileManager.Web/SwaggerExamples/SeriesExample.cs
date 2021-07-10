using FileManager.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Diagnostics.CodeAnalysis;

namespace FileManager.Web.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
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