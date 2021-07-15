using FileManager.DataAccessLayer.Queries;
using FileManager.Models.Dtos;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IQueryByName<IQueryName, T2> where T2 : Dto
    {
        T2 Query(IQueryName queryName);
    }

    public static class IQueryByNameExtentions
    {
        public static T Query<T>(this IQueryByName<QueryByName, T> queryByName, string name) where T : Dto
        {
            return queryByName.Query(new QueryByName { Name = name });
        }
    }
}
