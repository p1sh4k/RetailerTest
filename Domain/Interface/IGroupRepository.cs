using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Interface
{
    public interface IGroupRepository
    {
        Task<IEnumerable<GroupModel>> GetAllGroups();

        Task<GroupModel> GetGroup(string id);

        Task AddGroup(GroupModel item);

        Task<DeleteResult> RemoveGroup(string id);

        Task<UpdateResult> UpdateGroup(string id, string name);
    }
}
