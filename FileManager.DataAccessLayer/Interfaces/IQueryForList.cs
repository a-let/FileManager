using FileManager.Models.Dtos;

using System.Collections.Generic;

namespace FileManager.DataAccessLayer.Interfaces
{
    public interface IQueryForList<T> where T : Dto
    {
        IEnumerable<T> Query();
    }
}
