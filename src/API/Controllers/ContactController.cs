using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            try
            {
                var contact = await _contactService.GetByIdAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }
                return Ok(contact);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting the contact.", error = ex.Message });
            }
           
        }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> AddContact(int customerId, [FromBody] ContactCreateDTO contactCreateDto)
        {
            try
            {
                // Check if the contactDto is null
                if (contactCreateDto == null)
                {
                    return BadRequest("Contact data is required.");
                }


                // Call the AddAsync method from _contactService to add the contact
                var newContactId = await _contactService.AddAsync(customerId, contactCreateDto.Type, contactCreateDto.Value);

                var contactDto = new ContactDTO
                {
                    Id = newContactId,
                    Type = contactCreateDto.Type,
                    Value = contactCreateDto.Value
                };

                // Return the CreatedAtAction response indicating success and providing the URL to retrieve the created contact
                return CreatedAtAction(nameof(GetContactById), new { id = newContactId }, contactDto);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the contact.", error = ex.Message });
            }
           
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactUpdateDTO contactUpdateDto)
        {
            try
            {
                await _contactService.UpdateAsync(id, contactUpdateDto);
                return StatusCode(200, new { message = "Updated Successfully" });
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the contact.", error = ex.Message });
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactService.DeleteAsync(id);
                return StatusCode(200, new { message = "Deleted Successfully" });
            } catch (Exception ex)
            {

                return StatusCode(500, new { message = "An error occurred while deleting the contact.", error = ex.Message });
            }
            
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetContactsByCustomerId(int customerId)
        {
            try
            {
                var contacts = await _contactService.GetAllByCustomerIdAsync(customerId);
                if (contacts == null)
                {
                    return NotFound();
                }
                return Ok(contacts);
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while getting the contact.", error = ex.Message });
            }
            
        }
    }
}
