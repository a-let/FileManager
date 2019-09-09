namespace FileManager.Interfaces
{
    public interface IFileManagerClient
    {
        IEpisodeService EpisodeService { get; }
        IMovieService MovieService { get; }
        ISeasonService SeasonService { get; }
        ISeriesService SeriesService { get; }
        IShowService ShowService { get; }
    }
}