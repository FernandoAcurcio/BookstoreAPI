using API.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/salesitems")]
    public class SalesItemsController
    {
        private readonly AppDbContext _context;

        public SalesItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalesItem>>> GetSalesItemsAsync()
        {
            return await _context.SalesItems.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesItem>> GetSalesItemAsync(int id)
        {
            return await _context.SalesItems.FindAsync(id);
        }

    }
}
