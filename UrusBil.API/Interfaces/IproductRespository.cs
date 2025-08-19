using UrusBil.API.Models;

namespace UrusBil.API.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<bool> DeactivateProductAsync(int id);
    }
}
