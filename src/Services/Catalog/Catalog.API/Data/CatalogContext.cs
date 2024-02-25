namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(
                configuration
                .GetValue<string>("DatabaseSettings:ConnectionString")
                );//connection pool?
            var database = client
                .GetDatabase(
                    configuration
                    .GetValue<string>("DatabaseSettings:DatabaseName")
                 );

            Products = database
                .GetCollection<Product>(
                    configuration
                    .GetValue<string>("DatabaseSettings:CollectionName")
                );//all db tables will come from appsetting and set ctor?
            CatalogContextSeed.SeedData(Products);//Seed mock data
        }
        public IMongoCollection<Product> Products { get; }
    }
}
