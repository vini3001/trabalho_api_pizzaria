using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzariaReges.Service;
using WebApiProducts.Models;

namespace PizzariaReges.Controllers
{
    public class PizzasController : Controller
    {
        private readonly apiService _apiContext;


        public PizzasController(apiService apiContext)
        {
            this._apiContext = apiContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _apiContext.GetPizzasAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pizza pizzas)
        {
            _apiContext.PostPizzaAsync(pizzas);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _apiContext.GetPizzaAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        public bool PizzaExists(int id)
        {
            var pizza = _apiContext.GetPizzaAsync(id);

            if(pizza.Id == id)
            {
                return true;
            } else
            {
                return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pizza pizza)
        {
            _apiContext.PutPizzaAsync(id, pizza);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _apiContext.GetPizzaAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _apiContext.GetPizzaAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _apiContext.GetPizzaAsync(id);
            _apiContext.DeletePizzaAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
