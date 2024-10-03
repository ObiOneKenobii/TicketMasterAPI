using Microsoft.AspNetCore.Mvc;
using TicketMasterAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketMasterAPI.Data;

namespace TicketMasterAPI.Controllers
{
    [Route("api/beverages")]
    [ApiController]
    public class BeveragesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BeveragesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/beverages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beverage>>> GetBeverages()
        {
            return Ok(_context.Beverages.ToList());
        }

        // GET: api/beverages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beverage>> GetBeverage(int id)
        {
            var beverage = _context.Beverages.FirstOrDefault(b => b.Id == id);
            if (beverage == null)
            {
                return NotFound();
            }
            return Ok(beverage);
        }

        // POST: api/beverages
        [HttpPost]
        public async Task<ActionResult<Beverage>> CreateBeverage(Beverage beverage)
        {
            _context.Beverages.Add(beverage);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBeverage), new { id = beverage.Id }, beverage);
        }

        // PUT: api/beverages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeverage(int id, Beverage beverage)
        {
            var existingBeverage = _context.Beverages.FirstOrDefault(b => b.Id == id);
            if (existingBeverage == null || id != beverage.Id)
            {
                return BadRequest();
            }

            existingBeverage.Name = beverage.Name;
            existingBeverage.Quantity = beverage.Quantity;
            existingBeverage.Size = beverage.Size;
            existingBeverage.CoverImageUrl = beverage.CoverImageUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/beverages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeverage(int id)
        {
            var beverage = _context.Beverages.FirstOrDefault(b => b.Id == id);
            if (beverage == null)
            {
                return NotFound();
            }

            _context.Beverages.Remove(beverage);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
