using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration configuration)
        {
            
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString").Replace("<db_password>", "UserPass123"));
            //List<string> dbList = client.ListDatabaseNames().ToList();
            // var testCoolectionList = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName")).ListCollectionNames().ToList();

            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            
            Brands = database.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
            Types = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollection"));

            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
