using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting the task.", error = ex.Message });
            }

        }


        [HttpPost("{customerId}")]
        public async Task<IActionResult> AddTask(int customerId, [FromBody] TaskCreateDTO taskCreateDto)
        {
            try
            {
                if (taskCreateDto == null)
                {
                    return BadRequest("Task data is required.");
                }

                var newTaskId = await _taskService.AddAsync(customerId, taskCreateDto);

                var response_created_task = new
                {
                    id = newTaskId,
                    creationDate = taskCreateDto.CreationDate,
                    description = taskCreateDto.Description,
                    solved = taskCreateDto.Solved
                };
                return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, response_created_task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the task.", error = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDTO payload)
        {
            try
            {
                await _taskService.UpdateAsync(id, payload.Description, payload.Solved);
                return StatusCode(200, new { message = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the task.", error = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteAsync(id);
                return StatusCode(200, new { message = "Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the task.", error = ex.Message });
            }

        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetTasksByCustomerId(int customerId)
        {
            try
            {
                var tasks = await _taskService.GetAllByCustomerIdAsync(customerId);
                if (tasks == null || !tasks.Any())
                {
                    return NotFound();
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting the tasks.", error = ex.Message });
            }

        }

    }
}
