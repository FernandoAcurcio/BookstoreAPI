using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data;
using System.Linq;

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
        /// retrieve all the sale items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<SalesItem>>> GetSalesItemsAsync()
        {
            return await _context.SalesItems.ToListAsync();
        }
        
        /// <summary>
        /// get a specific sale item by id
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
            _context.SalesItems.Add(salesItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesItemAsync), new { id = salesItem.Id }, salesItem);
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

            _context.Entry(salesItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
    }
}
