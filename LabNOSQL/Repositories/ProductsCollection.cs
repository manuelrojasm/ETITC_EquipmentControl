using LabNOSQL.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LabNOSQL.Repositories
{
    public class ProductsCollection : IProductsCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Products> Collection;
        public ProductsCollection()
        {
            Collection = _repository.db.GetCollection<Products>("Products");
        }
        public async Task CreateProduct(Products product)
        {
            await Collection.InsertOneAsync(product);
        }
        public async Task DeleteProduct(string id)
        {
            var filter = Builders<Products>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }
        public async Task<List<Products>> GetAllProducts()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task<Products> ReadProduct(string id)
        {
            return await Collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }
        public async Task UpdateProduct(Products product)
        {
            var filter = Builders<Products>
                .Filter
                .Eq(s => s.Id, product.Id);
            await Collection.ReplaceOneAsync(filter, product);
        }

    }
}
