using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Application.Helpers;
using Domain.PaginatedResult;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> AddAsync(CustomerCreateDTO customerDto)
        {
            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Birthday = customerDto.Birthday
            };

            return await _customerRepository.AddAsync(customer);
        }

        public async Task<CustomerDetailedDTO> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDetailedDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Birthday = customer.Birthday,
                Contacts = customer.Contacts.Select(contact => new ContactDTO
                {
                    Id = contact.Id,
                    Type = (DTO.ContactType)contact.Type,
                    Value = contact.Value
                }).ToList(),
                Tasks = customer.Tasks.Select(task => new TaskDTO
                {
                    Id = task.Id,
                    Description = task.Description,
                    CreationDate = task.CreationDate,
                    Solved = task.Solved
                }).ToList()
            };
        }


        public async System.Threading.Tasks.Task UpdateAsync(int id, CustomerUpdateDTO customerDto)
        {

            await _customerRepository.UpdateAsync(id, customerDto.FirstName, customerDto.LastName, customerDto.Birthday);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResult<CustomerDTO>> SearchByNameAsync(string searchQuery, int pageNumber, int pageSize)
        {
            var response = await _customerRepository.SearchByNameAsync(searchQuery, pageNumber, pageSize);
            var data = response.Data.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Birthday = c.Birthday
            });

            var total = response.TotalCount;
            return new PaginatedResult<CustomerDTO>
            {
                Data = data,
                TotalCount = total
            };
        }

        public async System.Threading.Tasks.Task DeleteAllDataAsync()
        {
            await _customerRepository.DeleteAllDataAsync();
        }

        public async System.Threading.Tasks.Task CreateSampleDataAsync()

        {
            var random = new Random();
            for (int i = 0; i < 100000; i++)
            {
                var customer = new Customer
                {
                    FirstName = RandomDataGenerator.GenerateRandomString(3, 6),
                    LastName = RandomDataGenerator.GenerateRandomString(3, 6),
                    Birthday = DateTime.Now.AddYears(-30 + random.Next(0, 10)),
                    Contacts = new List<Contact>
                    {
                        new Contact { Type = Domain.Entities.ContactType.Phone, Value = RandomDataGenerator.GenerateRandomPhoneNumber() },
                        new Contact { Type = Domain.Entities.ContactType.Mail, Value = RandomDataGenerator.GenerateRandomEmail() },
                        new Contact { Type = Domain.Entities.ContactType.Web, Value = RandomDataGenerator.GenerateRandomWebsite() }
                    },
                    Tasks = Enumerable.Range(1, 10).Select(t => new Domain.Entities.Task
                    {
                        Description = RandomDataGenerator.GenerateRandomTaskDescription(),
                        CreationDate = DateTime.Now,
                        Solved = false
                    }).ToList()
                };

                _customerRepository.Add(customer);

                // Save changes every 1,000 customers to avoid memory issues
                if (i % 1000 == 0)

                    await _customerRepository.SaveChangesAsync();

            }

            // Ensure the final batch is saved
            await _customerRepository.SaveChangesAsync();
        }
    
    }
}
