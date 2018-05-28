using Domain.Entities;
using Domain.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Config;

namespace Infrastructure.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseContext _context;

        public GroupRepository(IOptions<Settings> settings)
        {
            _context = new DatabaseContext(settings);
        }

        public async Task<IEnumerable<GroupModel>> GetAllGroups()
        {
            return await _context.Groups.Find(_ => true).ToListAsync();
        }

        public async Task<GroupModel> GetGroup(string id)
        {
            var filter = Builders<GroupModel>.Filter.Eq("_Id", ObjectId.Parse(id));
            return await _context.Groups
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task AddGroup(GroupModel item)
        {
            await _context.Groups.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemoveGroup(string id)
        {
            return await _context.Groups.DeleteOneAsync(
                Builders<GroupModel>.Filter.Eq("_Id", ObjectId.Parse(id)));
        }

        public async Task<UpdateResult> UpdateGroup(string id, string name)
        {
            var filter = Builders<GroupModel>.Filter.Eq("_Id", ObjectId.Parse(id));
            var update = Builders<GroupModel>.Update
                .Set(s => s.Name, name)
                .CurrentDate(s => s.ModificationDate);

            return await _context.Groups.UpdateOneAsync(filter, update);
        }
    }
}