namespace FileManager.Web.Services.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userName);
    }
}