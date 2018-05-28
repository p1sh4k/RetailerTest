using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Config
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database = null;

        public DatabaseContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<RetailModel> Retails => _database.GetCollection<RetailModel>("Retail");

        public IMongoCollection<GroupModel> Groups => _database.GetCollection<GroupModel>("Group");
    }
}
