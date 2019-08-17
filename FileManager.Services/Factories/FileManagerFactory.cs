using FileManager.Services.Interfaces;

namespace FileManager.Services.Factories
{
    internal abstract class FileManagerFactory<T>
    {
        public abstract IService<T> Create();
    }
}