using System;
using Microsoft.AspNetCore.Mvc;
using Para.Data.Domain;
using Para.Data.CustomerDapperRepository;

namespace Para.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaporController : ControllerBase
    {
        private readonly ICustomerDapperRepository _customerDapperRepository;

        public RaporController(ICustomerDapperRepository customerDapperRepository)
        {
            _customerDapperRepository = customerDapperRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerDapperRepository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerDapperRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerDapperRepository.DeleteAsync(id);
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}