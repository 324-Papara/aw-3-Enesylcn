using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Para.Data.Domain;

namespace Para.Data.CustomerDapperRepository
{
    public interface ICustomerDapperRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<int> AddAsync(Customer customer);
        Task<int> UpdateAsync(Customer customer);
        Task<int> DeleteAsync(int id);
    }
}