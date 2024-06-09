using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProducts.Context;
using WebApiProducts.Models;

namespace WebApiProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : Controller
    {
        private readonly AppDbContext _context;

        public PizzasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizzas(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizzas", new { id = pizza.Id }, pizza);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            return await _context.Pizzas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var Pizzas = await _context.Pizzas.FindAsync(id);
            if (Pizzas == null)
            {
                return NotFound();
            }

            return Pizzas;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzas(int id, Pizza Pizzas)
        {
            if (id != Pizzas.Id)
            {
                return BadRequest();
            }

            _context.Entry(Pizzas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzasExists(id))
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

        private bool PizzasExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var produto = await _context.Pizzas.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
