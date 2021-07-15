using FileManager.DataAccessLayer.Queries;
using FileManager.Models.Dtos;

using System.Threading.Tasks;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IQueryByIdAsync<IQueryId, T> where T : Dto
    {
        Task<T> QueryAsync(IQueryId queryId);
    }

    public static class IQueryByIdAsyncExtenstions
    {
        public static async Task<T> QueryAsync<T>(this IQueryByIdAsync<QueryById, T> queryByIdAsync, int id) where T : Dto
        {
            return await queryByIdAsync.QueryAsync(new QueryById { Id = id });
        }
    }
}
