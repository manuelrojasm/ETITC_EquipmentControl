using LabNOSQL.Models;

namespace LabNOSQL.Repositories
{
    public interface IProductsCollection
    {
        Task CreateProduct(Models.Products product);
        Task<Products> ReadProduct(string id);
        Task<List<Products>> GetAllProducts();
        Task UpdateProduct(Products product);
        Task DeleteProduct(string id);
    }
}
