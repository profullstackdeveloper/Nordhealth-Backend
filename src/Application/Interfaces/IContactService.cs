using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContactService
    {
        Task<int> AddAsync(int CustomerId,  ContactType Type, string Value);
        Task<ContactDTO> GetByIdAsync(int id);
        Task<IEnumerable<ContactDTO>> GetAllByCustomerIdAsync(int customerId);
        Task UpdateAsync(int id, ContactUpdateDTO contact);
        Task DeleteAsync(int id);
    }
}
