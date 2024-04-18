using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<int> AddAsync(int customerId, Contact contact);

        Task<Contact> GetByIdAsync(int id);

        Task<IEnumerable<Contact>> GetAllByCustomerIdAsync(int customerId);

        System.Threading.Tasks.Task UpdateAsync(int id, ContactType Type, string? Value);

        System.Threading.Tasks.Task DeleteAsync(int id);

   
    }
}
