using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProducts.Context;
using WebApiProducts.Models;

namespace WebApiProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetClientes", new { id = cliente.Id }, cliente);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var Cliente = await _context.Clientes.FindAsync(id);
            if (Cliente == null)
            {
                return NotFound();
            }

            return Cliente;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente Cliente)
        {
            if (id != Cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(Cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var produto = await _context.Clientes.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
