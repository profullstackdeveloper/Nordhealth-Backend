using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<int> AddAsync(int customerId, TaskCreateDTO task);
        Task<TaskDTO> GetByIdAsync(int id);
        Task<IEnumerable<TaskDTO>> GetAllByCustomerIdAsync(int customerId);
        Task UpdateAsync(int id, string? description, bool? solved);
        Task DeleteAsync(int id);
    }
}
