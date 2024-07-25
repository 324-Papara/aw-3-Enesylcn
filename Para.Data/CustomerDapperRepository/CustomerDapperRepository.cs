using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Para.Data.Domain;
using Dapper;


namespace Para.Data.CustomerDapperRepository
{
    public class CustomerDapperRepository : ICustomerDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Customer>("SELECT * FROM Customer");
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customer WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> AddAsync(Customer customer)
        {
            var query = "INSERT INTO Customer (FirstName, Email) VALUES (@FirstName, @Email)";
            return await _dbConnection.ExecuteAsync(query, customer);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            var query = "UPDATE Customer SET FirstName = @FirstName, Email = @Email WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, customer);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "DELETE FROM Customer WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}