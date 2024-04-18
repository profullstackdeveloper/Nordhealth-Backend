using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITaskRepository
    {

        Task<int> AddAsync(int customerId, Domain.Entities.Task task);

        Task<Domain.Entities.Task> GetByIdAsync(int id);

        Task<IEnumerable<Domain.Entities.Task>> GetAllByCustomerIdAsync(int customerId);

        System.Threading.Tasks.Task UpdateAsync(int id, string? description, bool? solved);

        System.Threading.Tasks.Task DeleteAsync(int id);

    }
}
