using UrusBil.API.Models;

namespace UrusBil.API.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);
        Task<IEnumerable<Customer>> SearchByNameAsync(string name);
    }
}
