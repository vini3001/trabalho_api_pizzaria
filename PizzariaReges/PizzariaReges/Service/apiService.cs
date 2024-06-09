using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApiProducts.Models;

namespace PizzariaReges.Service
{
    public class apiService
    {
        private readonly HttpClient _httpClient;

        public apiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pizza>> GetPizzasAsync()
        {
            var response = await _httpClient.GetAsync("api/pizzas");
            response.EnsureSuccessStatusCode();

            var pizzas = await response.Content.ReadFromJsonAsync<List<Pizza>>();
            pizzas = pizzas.OrderBy(pizza => pizza.Nome).ToList();
            return pizzas;
        }

        public async Task<Pizza> GetPizzaAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/pizzas/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Pizza>();
        }

        public async Task<HttpResponseMessage> PostPizzaAsync(Pizza pizza)
        {
            return await _httpClient.PostAsJsonAsync("api/pizzas/", pizza);
        }

        public async Task<HttpResponseMessage> PutPizzaAsync(int id, Pizza pizza)
        {
            return await _httpClient.PutAsJsonAsync($"api/pizzas/{id}", pizza);
        }

        public async Task<HttpResponseMessage> DeletePizzaAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/pizzas/{id}");
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            var response = await _httpClient.GetAsync("api/clientes");
            response.EnsureSuccessStatusCode();

            var clientes = await response.Content.ReadFromJsonAsync<List<Cliente>>();
            clientes = clientes.OrderBy(cliente => cliente.Name).ToList();
            return clientes;
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Cliente>();
        }

        public async Task<HttpResponseMessage> PostClienteAsync(Cliente Cliente)
        {
            return await _httpClient.PostAsJsonAsync("api/clientes/", Cliente);
        }

        public async Task<HttpResponseMessage> PutClienteAsync(int id, Cliente Cliente)
        {
            return await _httpClient.PutAsJsonAsync($"api/clientes/{id}", Cliente);
        }

        public async Task<HttpResponseMessage> DeleteClienteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/clientes/{id}");
        }
    }
}
