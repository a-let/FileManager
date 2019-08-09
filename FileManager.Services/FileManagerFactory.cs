using FileManager.Services.Interfaces;

namespace FileManager.Services
{
    internal abstract class FileManagerFactory<T>
    {
        public abstract IService<T> Create();
    }
}