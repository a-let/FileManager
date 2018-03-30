using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FileManager.BusinessLayer;
using FFT = FileManager.BusinessLayer.Helpers.FileFormats.FileFormatTypes;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //var show = Show.NewShow();
            //show.Name = "Test Show";
            //show.Category = "Testing";
            //show.Path = @"C:\Temp\test_show";
            //show.Save();

            //var season = Season.NewSeason();
            //season.SeasonNumber = "1";
            //season.ShowId = show.ShowId;
            //season.Path = @"C:\Temp\test_show\season_01";
            //season.Save();
            
            //var episode = Episode.NewEpisode();
            //episode.SeasonId = season.SeasonId;
            //episode.EpisodeNumber = "1";
            //episode.Format = FFT.mkv.ToString();
            //episode.Name = "Test Episode";
            //episode.Path = @"C:\Temp\test_show\season_01\test_episode";
            //episode.Save();
            

            //var testEpisode = Episode.GetEpisode("Test Episode");


            //foreach(var e in Episode.GetEpisodes())
            //{
            //    Console.WriteLine(e.Name);
            //    Console.WriteLine(e.Path);
            //}
            
            //Console.ReadLine();

            //var newSeries = new Series()
            //{
            //    SeriesId = 1,
            //    Name = "Test Series"
            //};

            //using (var context = new FileManagerContext())
            //{
            //    context.Series.Add(newSeries);
            //    context.SaveChanges();
            //};

            //var userPath = GetUserPath();
            //var searchParameter = GetSearchParameter();

            ////var directoriesFromPath = GetDirectoriesFromPath(userPath);
            //var directoriesFromPathWithSearch = GetDirectoriesFromPath(userPath, searchParameter);

            ////var directoryDict = new Dictionary<string, IEnumerable<string>>() { { searchParameter, GetDirectoriesFromPath(userPath, searchParameter) } };

            ////var mainFolder = FindMainFolder(directoriesFromPathWithSearch);

            //var newestSeasonDirectory = FindNewestSeasonDirectory(directoriesFromPathWithSearch);

            //MoveContentOfDirectory(directoriesFromPathWithSearch, newestSeasonDirectory);
        }

        private static string GetUserPath()
        {
            //Console.WriteLine("Enter path to directory you would like to use:");
            //return $@"{Console.ReadLine()}";
            return @"";
        }

        private static string GetSearchParameter()
        {
            Console.WriteLine("Enter search word or phrase:");
            return $"{Console.ReadLine()}";
        }

        private static IEnumerable<string> GetDirectoriesFromPath(string userPath) => Directory.EnumerateDirectories(userPath);
        private static IEnumerable<string> GetDirectoriesFromPath(string userPath, string searchParameter) => Directory.EnumerateDirectories(userPath, $"*{searchParameter}*");
        private static string FindNewestSeasonDirectory(IEnumerable<string> directories)
        {
            var seasons = new List<int>();

            if (directories == null || directories.Count() == 0)
                return null;

            var seasonDirectoryL = directories.OrderBy(d => d.Length).FirstOrDefault();
            var seasonDirectoryLength = directories.OrderBy(d => d.Length).FirstOrDefault().Length;

            foreach(var directory in directories)
            {
                if (!IsSeasonDirectory(directory, seasonDirectoryLength))
                    continue;

                var isInt = int.TryParse(directory.Split('_').LastOrDefault().TrimStart('s'), out int season);

                if (isInt)
                    seasons.Add(season);
            }

            string currentSeasonDirectory = "";
            foreach(var directory in directories)
            {
                if (!IsSeasonDirectory(directory, seasonDirectoryLength))
                    continue;

                if (directory.Contains(seasons.Max().ToString()))
                    currentSeasonDirectory = directory;
            }

            return currentSeasonDirectory;
        }

        private static void MoveContentOfDirectory(IEnumerable<string> directories, string newestSeasonDirectory)
        {
            foreach (var directory in directories)
            {
                //var iscorrectSeason = IsCorrectSeasonDirectory(directory, newestSeasonDirectory);

                //https://stackoverflow.com/questions/6800649/how-to-move-file-folder-to-an-already-existing-folder?rq=1

                foreach(var file in Directory.EnumerateFiles(directory))
                {
                    if (!Enum.IsDefined(typeof(FFT), file.Split('.').Last()))
                        continue;

                    //var dest = Path.Combine(newestSeasonDirectory, Path.GetFileName(file));

                }
            }
        }

        private static bool IsSeasonDirectory(string directory, int seasonDirectoryLength)
        {
            if (seasonDirectoryLength == 0)
                return false;

            if (directory.Length == seasonDirectoryLength)
                return true;

            return false;
        }

        //private static bool IsCorrectSeasonDirectory(string directory, string newestSeasonDirectory)
        //{
        //    int episodeSeason;
        //    int seasonOfDirectory;

        //    var splitDirectory = directory.Split('.');
        //    foreach(var split in splitDirectory)
        //    {
        //        if (!(split.StartsWith("s", StringComparison.OrdinalIgnoreCase) && (split[3].Equals('e') || split[3].Equals('E'))))
        //            continue;

        //        var isepisodeSeasonInt = int.TryParse(split.TrimStart().Remove(3), out episodeSeason);
        //    }

        //    var isSeasonOfDirectoryInt = int.TryParse(newestSeasonDirectory.Split('_').LastOrDefault().TrimStart(), out seasonOfDirectory);


        //}

        //TODO: Make this a check for the word "season ##" and change the string to "s##"
        //private static string FindSeason(string directory)
        //{
        //    var splitDirectory = directory.Split('.');

        //    foreach (var split in splitDirectory)
        //    {
        //        if (!split.Equals("season", StringComparison.OrdinalIgnoreCase))
        //            continue;

        //        return directory;
        //    }

        //    return null;
        //}
    }
}
