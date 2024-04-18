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
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(int customerId, Domain.Entities.Task task)
        {
            // Ensure the task is associated with the given customer by setting the CustomerId property.
            task.CustomerId = customerId;

            // Add the task to the context.
            _context.Tasks.Add(task);

            // Save changes to the database.
            await _context.SaveChangesAsync();

            // Return the task ID.
            return task.Id;
        }


        public async Task<Domain.Entities.Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetAllByCustomerIdAsync(int customerId)
        {
            return await _context.Tasks
                .Where(t => t.CustomerId == customerId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(int id, string? description, bool? solved)
        {
            
            var task = await _context.Tasks.FindAsync(id);

            var payload = new Dictionary<string, object>();

            if(solved != null)
            {
                task.Solved = (bool)solved;
            }

            if(description != null && description != "")
            {
                task.Description = description;
            }

            if(description == "")
            {
                throw new ArgumentException("Description is required!");
            }

            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

      
    }
}
