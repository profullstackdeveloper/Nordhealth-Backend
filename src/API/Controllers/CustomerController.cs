using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting the customer.", error = ex.Message });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerCreateDTO customerDto)
        {
            try
            {
                var customerId = await _customerService.AddAsync(customerDto);
                var response_created_customer = new
                {
                    id = customerId,
                    firstName = customerDto.FirstName,
                    lastName = customerDto.LastName,
                    birthDay = customerDto.Birthday
                };
                return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, response_created_customer);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the customer.", error = ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDTO customerUpdateDto)
        {
            try
            {
                // Create a new CustomerDTO object with the provided values
            

                // Update the customer
                await _customerService.UpdateAsync(id, customerUpdateDto);

                // Return OK response
                return StatusCode(200, new {message = "Updated Successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the customer.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                return StatusCode(200, new { message = "Deleted Successfully" });
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the customer.", error = ex.Message });
            }
            
        }

        [HttpGet()]
        public async Task<IActionResult> SearchCustomersByName(string? query, int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var customers = await _customerService.SearchByNameAsync(query, pageNumber, pageSize);
                return Ok(customers);

            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting the customer.", error = ex.Message });
            }
            
        }

        [HttpDelete("delete-all")]
        public async Task<IActionResult> DeleteAllData()
        {
            try
            {
                await _customerService.DeleteAllDataAsync();
                return StatusCode(200, new { message = "Deleted Successfully" });
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting all data.", error = ex.Message });
            }
            
        }

        [HttpPost("create-sample-data")]
        public async Task<IActionResult> CreateSampleData()
        {
            await _customerService.CreateSampleDataAsync();
            return Ok("100,000 sample customers created successfully");
        }

    }
}
