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

        /// <summary>
        /// get list of all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// search for a customers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// create customer
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
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


        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="country"></param>
        /// <returns></returns>
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

        /// <summary>
        /// delete customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// verify if a customer exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
