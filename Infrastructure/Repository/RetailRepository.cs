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
    public class RetailRepository : IRetailRepository
    {
        private readonly DatabaseContext _context;

        public RetailRepository(IOptions<Settings> settings)
        {
            _context = new DatabaseContext(settings);
        }

        public async Task<IEnumerable<RetailModel>> GetAllRetails()
        {
            return await _context.Retails.Find(_ => true).ToListAsync();
        }

        public async Task<RetailModel> GetRetail(string id)
        {
            var filter = Builders<RetailModel>.Filter.Eq("Id", ObjectId.Parse(id));
            return await _context.Retails
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task AddRetail(RetailModel item)
        {
            await _context.Retails.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemoveRetail(string id)
        {
            return await _context.Retails.DeleteOneAsync(
                Builders<RetailModel>.Filter.Eq("Id", ObjectId.Parse(id)));
        }

        public async Task<UpdateResult> UpdateRetail(string id, string name, string groupid)
        {
            var filter = Builders<RetailModel>.Filter.Eq("Id", ObjectId.Parse(id));
            var update = Builders<RetailModel>.Update
                .Set(s => s.Name, name)
                .Set(s => s.GroupId, groupid)
                .CurrentDate(s => s.ModificationDate);

            return await _context.Retails.UpdateOneAsync(filter, update);
        }
    }
}