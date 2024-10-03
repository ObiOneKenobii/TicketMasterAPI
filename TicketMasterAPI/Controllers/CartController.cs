using Microsoft.AspNetCore.Mvc;
using TicketMasterAPI.Models; // Assuming Cart is in Models namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketMasterAPI.Data;

[Route("api/carts")]
[ApiController]
public class CartApiController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Inject ApplicationDbContext

    // Constructor to inject ApplicationDbContext
    public CartApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/carts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        var carts = await _context.Carts.Include(c => c.Beverages).ToListAsync();
        return Ok(carts);
    }

    // GET: api/carts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(int id)
    {
        var cart = await _context.Carts.Include(c => c.Beverages)
                                       .FirstOrDefaultAsync(c => c.Id == id);

        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    // POST: api/carts
    [HttpPost]
    public async Task<ActionResult<Cart>> CreateCart(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        // Return the newly created cart
        return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
    }

    // PUT: api/carts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, Cart cart)
    {
        if (id != cart.Id)
        {
            return BadRequest();
        }

        _context.Entry(cart).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Carts.Any(c => c.Id == id))
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

    // DELETE: api/carts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
