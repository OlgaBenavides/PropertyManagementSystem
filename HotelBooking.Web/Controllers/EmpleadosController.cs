using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto1Fundamentos.Models;

namespace Proyecto1Fundamentos.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpleadosController(IHttpClientFactory httpClientFactory)
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
                var resp = await client.GetAsync("api/Empleados");
                if (!resp.IsSuccessStatusCode)
                    return View(new List<Empleado>());

                var stream = await resp.Content.ReadAsStreamAsync();
                var opciones = GetJsonOptions();
                var lista = await JsonSerializer.DeserializeAsync<List<Empleado>>(stream, opciones);
                return View(lista ?? new List<Empleado>());
            }
            catch
            {
                return View(new List<Empleado>());
            }
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = Enum.GetValues(typeof(CategoriaEmpleado));
            ViewBag.Provincias = CatalogoUbicaciones.ObtenerProvincias();
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            ViewBag.Categorias = Enum.GetValues(typeof(CategoriaEmpleado));
            ViewBag.Provincias = CatalogoUbicaciones.ObtenerProvincias();

            if (!ModelState.IsValid)
                return View(empleado);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(empleado, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("api/Empleados", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrWhiteSpace(error) ? error : "Error al crear el empleado.");
            return View(empleado);
        }

        // GET: Empleados/Edit/{identificacion}
        public async Task<IActionResult> Edit(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                return NotFound();

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Empleados/{Uri.EscapeDataString(identificacion)}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var empleado = await JsonSerializer.DeserializeAsync<Empleado>(stream, opciones);
            if (empleado == null) return NotFound();

            ViewBag.Categorias = Enum.GetValues(typeof(CategoriaEmpleado));
            ViewBag.Provincias = CatalogoUbicaciones.ObtenerProvincias();
            return View(empleado);
        }

        // POST: Empleados/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Empleado empleado)
        {
            ViewBag.Categorias = Enum.GetValues(typeof(CategoriaEmpleado));
            ViewBag.Provincias = CatalogoUbicaciones.ObtenerProvincias();

            if (!ModelState.IsValid)
                return View(empleado);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(empleado, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"api/Empleados/{Uri.EscapeDataString(empleado.Identificacion)}", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrWhiteSpace(error) ? error : "Error al actualizar el empleado.");
            return View(empleado);
        }

        // GET: Empleados/Delete/{identificacion}
        public async Task<IActionResult> Delete(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                return NotFound();

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Empleados/{Uri.EscapeDataString(identificacion)}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var empleado = await JsonSerializer.DeserializeAsync<Empleado>(stream, opciones);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        // POST: Empleados/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string identificacion)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.DeleteAsync($"api/Empleados/{Uri.EscapeDataString(identificacion)}");
            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = !string.IsNullOrWhiteSpace(error) ? error : "Error al eliminar el empleado.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Empleados/Buscar?identificacion=...
        public async Task<IActionResult> Buscar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                return View((Empleado)null);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Empleados/Buscar/{Uri.EscapeDataString(identificacion)}");
            if (!resp.IsSuccessStatusCode)
                return View((Empleado)null);

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var empleado = await JsonSerializer.DeserializeAsync<Empleado>(stream, opciones);
            return View(empleado);
        }
    }
}
