using FileManager.Models;
using FileManager.Models.Constants;

using Swashbuckle.AspNetCore.Filters;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FileManager.Web.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
    public class SeasonExample : IExamplesProvider<Season>
    {
        public Season GetExamples() => new Season
        {
            SeasonId = 0,
            ShowId = 0,
            SeasonNumber = 1,
            EpisodeList = new List<Episode>
            {
                new Episode
                {
                    EpisodeId = 0,
                    SeasonId = 0,
                    EpisodeNumber = 1,
                    Name = "Test Name",
                    Format = FileFormatTypes.MKV,
                    Path = @"C:\Temp\Test.mkv"
                }
            },
            Path = @"C:\Temp\Test_s01"
        };
    }
}