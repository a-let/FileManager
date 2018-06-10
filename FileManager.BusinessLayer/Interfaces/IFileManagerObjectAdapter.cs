using System.Collections.Generic;

namespace FileManager.BusinessLayer.Interfaces
{
    public interface IFileManagerObjectAdapter<T>
    {
        T GetById(int id);
        IEnumerable<T> Get();
        T GetByName(string name);
        bool Save(T target);
        IEnumerable<T> GetByParentId(int parentId);
    }
}