using Microsoft.AspNetCore.Mvc;
using UrusBil.API.DTOs;
using UrusBil.API.Interfaces;
using UrusBil.API.Models;

namespace UrusBil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepository customerRepository, ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                var customerDtos = customers.Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    City = c.City,
                    PostalCode = c.PostalCode
                });

                return Ok(customerDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    City = customer.City,
                    PostalCode = customer.PostalCode
                };

                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer {CustomerId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            try
            {
                var customer = new Customer
                {
                    Name = createCustomerDto.Name,
                    Email = createCustomerDto.Email,
                    Phone = createCustomerDto.Phone,
                    Address = createCustomerDto.Address,
                    City = createCustomerDto.City,
                    PostalCode = createCustomerDto.PostalCode
                };

                var createdCustomer = await _customerRepository.CreateAsync(customer);

                var customerDto = new CustomerDto
                {
                    Id = createdCustomer.Id,
                    Name = createdCustomer.Name,
                    Email = createdCustomer.Email,
                    Phone = createdCustomer.Phone,
                    Address = createdCustomer.Address,
                    City = createdCustomer.City,
                    PostalCode = createdCustomer.PostalCode
                };

                return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.Id }, customerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}