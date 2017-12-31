using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using POC.WebAPI.Models;

namespace POC.WebAPI.Data
{
    public class DbContext
    {
        public IConfiguration Configuration { get; }
        private readonly IMongoDatabase _database;

        public DbContext(IOptions<Settings> settings)
        {
            Configuration = settings.Value.ConfigurtionRoot;
            settings.Value.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            settings.Value.Database = Configuration.GetSection("MongoConnection:Database").Value;

            var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Account> Accounts => _database.GetCollection<Account>("accounts");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");

    }
}
