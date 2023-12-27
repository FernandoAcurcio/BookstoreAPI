using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Validate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            // Validate VAT for customers from Portugal
            if (customer.CountryId == 65)
            {
                if (!ValidateVat.ValidVat(customer.TaxId))
                {
                    return BadRequest("Invalid VAT for customers from Portugal.");
                }
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAsync(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            // Validate VAT for customers from Portugal
            if (customer.CountryId == 65)
            {
                if (!ValidateVat.ValidVat(customer.TaxId))
                {
                    return BadRequest("Invalid VAT for customers from Portugal.");
                }
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
