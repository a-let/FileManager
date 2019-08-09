namespace FileManager.Services.Interfaces
{
    public interface IFileManagerClient
    {
        IEpisodeService EpisodeService { get; }
        IMovieService MovieService { get; }
    }
}