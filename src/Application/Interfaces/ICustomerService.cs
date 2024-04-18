using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.PaginatedResult;


namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<int> AddAsync(CustomerCreateDTO customer);
        Task<CustomerDetailedDTO> GetByIdAsync(int customerId);
        Task UpdateAsync(int id, CustomerUpdateDTO customer);
        Task DeleteAsync(int id);
        Task CreateSampleDataAsync();
        Task DeleteAllDataAsync();
        Task<PaginatedResult<CustomerDTO>> SearchByNameAsync(string? searchQuery, int pageNumber, int pageSize);
       
    }
}
