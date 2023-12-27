using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data;
using System.Linq;
using Infrastructure.Data.Validate;

namespace API.Controllers
{
    [ApiController]
    [Route("api/salesitems")]
    public class SalesItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesItemsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// get list of all sales items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<SalesItem>>> GetSalesItemsAsync()
        {
            return await _context.SalesItems.ToListAsync();
        }

        /// <summary>
        /// search for a sale item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesItem>> GetSalesItemAsync(int id)
        {
            var salesItem = await _context.SalesItems.FindAsync(id);

            if (salesItem == null)
            {
                return NotFound();
            }

            return salesItem;
        }

        /// <summary>
        /// create sale item
        /// </summary>
        /// <param name="salesItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<SalesItem>> PostSalesItemAsync(SalesItem salesItem)
        {
            // Validate item price
            if (salesItem.Price <= 0)
            {
                return BadRequest("Invalid price for this item.");
            }

            _context.SalesItems.Add(salesItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// update sale item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salesItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesItemAsync(int id, SalesItem salesItem)
        {
            if (id != salesItem.Id)
            {
                return BadRequest();
            }

            // Validate item price
            if (salesItem.Price <= 0)
            {
                return BadRequest("Invalid price for this item.");
            }

            _context.Entry(salesItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesItemExists(id))
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
        /// delete sale item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesItemAsync(int id)
        {
            var salesItem = await _context.SalesItems.FindAsync(id);

            if (salesItem == null)
            {
                return NotFound();
            }

            _context.SalesItems.Remove(salesItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// verify if a sales item exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool SalesItemExists(int id)
        {
            return _context.SalesItems.Any(e => e.Id == id);
        }
    }
}
