using LabNOSQL.Models;

namespace LabNOSQL.Repositories
{
    public interface IProductsColletion
    {
        Task CreateProduct(Models.Products product);
        Task<Products> ReadProduct(string id);
        Task<List<Products>> GetAllProducts();
        Task UpdateProduct(Products product);
        Task DeleteProduct(string id);
    }
}
