using Microsoft.AspNetCore.Mvc;
using Proyecto1Fundamentos.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proyecto1Fundamentos.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            try
            {
                var resp = await client.GetAsync("api/Clientes");
                if (!resp.IsSuccessStatusCode)
                    return View(new List<Cliente>());

                var stream = await resp.Content.ReadAsStreamAsync();
                var opciones = GetJsonOptions();
                var lista = await JsonSerializer.DeserializeAsync<List<Cliente>>(stream, opciones);
                return View(lista ?? new List<Cliente>());
            }
            catch
            {
                return View(new List<Cliente>());
            }
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(cliente, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("api/Clientes", content);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var error = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrWhiteSpace(error) ? error : "Error al crear el cliente.");
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Clientes/{Uri.EscapeDataString(id)}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var cliente = await JsonSerializer.DeserializeAsync<Cliente>(stream, opciones);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(cliente, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"api/Clientes/{Uri.EscapeDataString(cliente.Identificacion)}", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrWhiteSpace(error) ? error : "Error al actualizar el cliente.");
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Clientes/{Uri.EscapeDataString(id)}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var cliente = await JsonSerializer.DeserializeAsync<Cliente>(stream, opciones);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.DeleteAsync($"api/Clientes/{Uri.EscapeDataString(id)}");
            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = !string.IsNullOrWhiteSpace(error) ? error : "Error al eliminar el cliente.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Buscar?identificacion=...
        public async Task<IActionResult> Buscar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                return View((Cliente)null);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Clientes/Buscar/{Uri.EscapeDataString(identificacion)}");
            if (!resp.IsSuccessStatusCode)
                return View((Cliente)null);

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var cliente = await JsonSerializer.DeserializeAsync<Cliente>(stream, opciones);
            return View(cliente);
        }
    }
}
