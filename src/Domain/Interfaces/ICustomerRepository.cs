using Domain.Entities;
using Domain.PaginatedResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        System.Threading.Tasks.Task<int> AddAsync(Customer customer);
        System.Threading.Tasks.Task<Customer> GetByIdAsync(int id);
        System.Threading.Tasks.Task UpdateAsync(int id, string? FirstName, string? LastName, DateTime? Birthday);
        System.Threading.Tasks.Task DeleteAsync(int id);

        System.Threading.Tasks.Task DeleteAllDataAsync();
        System.Threading.Tasks.Task<PaginatedResult<Customer>> SearchByNameAsync(string searchQuery, int pageNumber, int pageSize);
        System.Threading.Tasks.Task SaveChangesAsync();
        void Add(Customer customer);



    }
}
