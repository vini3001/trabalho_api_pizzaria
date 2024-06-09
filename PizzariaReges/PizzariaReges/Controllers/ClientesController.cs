using Microsoft.AspNetCore.Mvc;
using WebApiProducts.Models;
using PizzariaReges.Service;

namespace ClienteriaReges.Controllers
{
    public class ClientesController : Controller
    {
        private readonly apiService _apiContext;


        public ClientesController(apiService apiContext)
        {
            this._apiContext = apiContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _apiContext.GetClientesAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente clientes)
        {
            _apiContext.PostClienteAsync(clientes);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _apiContext.GetClienteAsync(id);

            if (Cliente == null)
            {
                return NotFound();
            }

            return View(Cliente);
        }

        public bool ClienteExists(int id)
        {
            var Cliente = _apiContext.GetClienteAsync(id);

            if (Cliente.Id == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente Cliente)
        {
            _apiContext.PutClienteAsync(id, Cliente);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _apiContext.GetClienteAsync(id);

            if (Cliente == null)
            {
                return NotFound();
            }

            return View(Cliente);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Cliente = await _apiContext.GetClienteAsync(id);

            if (Cliente == null)
            {
                return NotFound();
            }

            return View(Cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Cliente = await _apiContext.GetClienteAsync(id);
            _apiContext.DeleteClienteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
