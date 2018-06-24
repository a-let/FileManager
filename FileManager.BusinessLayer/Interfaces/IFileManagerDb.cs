using System.Data;

namespace FileManager.BusinessLayer.Interfaces
{
    public interface IFileManagerDb
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand();
        IDbDataParameter CreateParameter(string name, object value);
    }
}