using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.PaginatedResult;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TaskDbContext _context;

        public CustomerRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);

                await _context.SaveChangesAsync();
                return customer.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Contacts)
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    

        public async System.Threading.Tasks.Task UpdateAsync(int id, string? FirstName, string? LastName, DateTime? Birthday)
        {
            var customer = await _context.Customers.FindAsync(id);
            
            if (customer == null) throw new ArgumentException("Customer not found");
            

            if (FirstName != null || FirstName == "") customer.FirstName = FirstName;
            

            if (LastName != null || LastName == "") customer.LastName = LastName;

            if (Birthday != null) customer.Birthday = (DateTime)Birthday;


            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<Customer>> SearchByNameAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var totalCount = await _context.Customers.CountAsync();
            var customers = await _context.Customers
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            if(searchQuery != null)
            {
                customers = await _context.Customers
                                    .Where(c => EF.Functions.Like(c.FirstName, $"%{searchQuery}%") ||
                                     EF.Functions.Like(c.LastName, $"%{searchQuery}%"))
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize).ToListAsync();

                totalCount = await _context.Customers.Where(c => EF.Functions.Like(c.FirstName, $"%{searchQuery}%") ||
                            EF.Functions.Like(c.LastName, $"%{searchQuery}%")).CountAsync();

            }

                return new PaginatedResult<Customer>
                {
                    Data = customers,
                    TotalCount = totalCount
                };
          
        }

        public async System.Threading.Tasks.Task DeleteAllDataAsync()
        {
            // Delete all contacts
            var removeAllContactsSQL = "DELETE FROM Contacts";

            var removeAllTasksSQL = "DELETE FROM Tasks";

            var removeAllCustomersSQL = "DELETE FROM Customers";

            await _context.Database.ExecuteSqlRawAsync(removeAllContactsSQL);
            await _context.Database.ExecuteSqlRawAsync(removeAllTasksSQL);
            await _context.Database.ExecuteSqlRawAsync(removeAllCustomersSQL);

        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }
        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
      

    }
}
