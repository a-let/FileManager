using FileManager.Models;
using FileManager.Models.Constants;

using Swashbuckle.AspNetCore.Filters;

namespace FileManager.Web.SwaggerExamples
{
    public class EpisodeExample : IExamplesProvider<Episode>
    {
        public Episode GetExamples() => new Episode
        {
            EpisodeId = 0,
            SeasonId = 0,
            EpisodeNumber = 1,
            Name = "Test Name",
            Format = FileFormatTypes.MKV,
            Path = @"C:\Temp\Test.mkv"
        };
    }
}