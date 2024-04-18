using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<int> AddAsync(int customerId, TaskCreateDTO taskCreateDto)
        {
            var task = new Domain.Entities.Task
            {
                Description = taskCreateDto.Description,
                CreationDate = taskCreateDto.CreationDate,
                Solved = taskCreateDto.Solved
            };

            return await _taskRepository.AddAsync(customerId, task);
        }

        public async Task<TaskDTO> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDTO
            {
                Id = task.Id,
                Description = task.Description,
                CreationDate = task.CreationDate,
                Solved = task.Solved
            };
        }

        public async Task<IEnumerable<TaskDTO>> GetAllByCustomerIdAsync(int customerId)
        {
            var tasks = await _taskRepository.GetAllByCustomerIdAsync(customerId);
            return tasks.Select(t => new TaskDTO
            {
                Id = t.Id,
                Description = t.Description,
                CreationDate = t.CreationDate,
                Solved = t.Solved
            });
        }

        public async System.Threading.Tasks.Task UpdateAsync(int id, string? description, bool? solved)
        {
          
            await _taskRepository.UpdateAsync(id, description, solved);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        
    }
}
