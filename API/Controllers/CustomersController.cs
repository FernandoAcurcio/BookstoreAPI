using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }


        //[HttpPost]
        //public async Task<ActionResult<Customer>> CreateCustomerAsync([FromBody] Customer customer)
        //{
        //    _context.Customers.Add(customer);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Customer>> UpdateCustomerAsync(int id, [FromBody] Customer updatedCustomer)
        //{
        //    var existingCustomer = await _context.Customers.FindAsync(id);

        //    if (existingCustomer == null)
        //    {
        //        return NotFound(); // 404 Not Found if the customer with the given ID is not found
        //    }

        //    // Update properties of existingCustomer with values from updatedCustomer
        //    existingCustomer.FirstName = updatedCustomer.FirstName;
        //    existingCustomer.LastName = updatedCustomer.LastName;
        //    // Update other properties as needed

        //    await _context.SaveChangesAsync();

        //    return Ok(existingCustomer);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteCustomerAsync(int id)
        //{
        //    var customer = await _context.Customers.FindAsync(id);

        //    if (customer == null)
        //    {
        //        return NotFound(); // 404 Not Found if the customer with the given ID is not found
        //    }

        //    _context.Customers.Remove(customer);
        //    await _context.SaveChangesAsync();

        //    return NoContent(); // 204 No Content if the customer is successfully deleted
        //}
    }
}
