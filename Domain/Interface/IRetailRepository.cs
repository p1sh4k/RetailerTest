using Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRetailRepository
    {
        Task<IEnumerable<RetailModel>> GetAllRetails();

        Task<RetailModel> GetRetail(string id);

        Task AddRetail(RetailModel item);

        Task<DeleteResult> RemoveRetail(string id);

        Task<UpdateResult> UpdateRetail(string id, string name, string groupid);
    }
}