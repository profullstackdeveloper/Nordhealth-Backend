using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly TaskDbContext _context;

        public ContactRepository(TaskDbContext context)
        {
            _context = context;
        }

      
        public async Task<int> AddAsync(int customerId, Contact contact)
        {
            // Retrieve the customer entity from the database
            var customer = await _context.Customers.FindAsync(customerId);

            // Check if the customer exists
            if (customer == null)
            {
                throw new ArgumentException($"Customer with ID {customerId} does not exist.");
            }

            // Set the contact's CustomerId property to the provided customerId
            contact.CustomerId = customerId;

            // Add the contact to the customer's contacts collection
            customer.Contacts.Add(contact);

            // Add the contact to the context
            _context.Contacts.Add(contact);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the contact's ID
            return contact.Id;
        }


        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAllByCustomerIdAsync(int customerId)
        {
            return await _context.Contacts
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(int id, ContactType Type, string? Value)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                throw new ArgumentException($"Contact with ID {id} does not exist.");
            }

            if (Type != null) contact.Type = Type;

            if (Value != null && Value != "") contact.Value = Value;

            if (Value == "")  throw new ArgumentException("Value is required!");
            


            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
