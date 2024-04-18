using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<int> AddAsync(int customerId, DTO.ContactType Type, string Value)
        {
            var contact = new Contact
            {
                Type = (Domain.Entities.ContactType) Type,
                Value = Value
            };

            return await _contactRepository.AddAsync(customerId, contact);
        }

        public async Task<ContactDTO> GetByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null) return null;

            return new ContactDTO
            {
                Id = contact.Id,
                Type = (DTO.ContactType)contact.Type,
                Value = contact.Value
            };
        }

        public async Task<IEnumerable<ContactDTO>> GetAllByCustomerIdAsync(int customerId)
        {
            var contacts = await _contactRepository.GetAllByCustomerIdAsync(customerId);
            return contacts.Select(c => new ContactDTO
            {
                Id = c.Id,
                Type = (DTO.ContactType)c.Type,
                Value = c.Value
            });
        }

        public async System.Threading.Tasks.Task UpdateAsync(int id, ContactUpdateDTO contactDto)
        {
            
            await _contactRepository.UpdateAsync(id, (Domain.Entities.ContactType)contactDto.Type, contactDto.Value);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }
    }
}
