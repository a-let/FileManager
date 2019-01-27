namespace FileManager.Web.Services.Interfaces
{
    public interface ICryptographyService
    {
        void CreateHash(string value, out byte[] hash, out byte[] salt);
        bool VerifyHash(string value, byte[] hash, byte[] salt);
    }
}