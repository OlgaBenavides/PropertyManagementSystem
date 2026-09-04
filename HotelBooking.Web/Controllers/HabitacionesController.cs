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
    public class HabitacionesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HabitacionesController(IHttpClientFactory httpClientFactory)
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
                var resp = await client.GetAsync("api/Habitaciones");
                if (!resp.IsSuccessStatusCode)
                    return View(new List<Habitacion>());

                var stream = await resp.Content.ReadAsStreamAsync();
                var opciones = GetJsonOptions();
                var lista = await JsonSerializer.DeserializeAsync<List<Habitacion>>(stream, opciones);
                return View(lista ?? new List<Habitacion>());
            }
            catch
            {
                return View(new List<Habitacion>());
            }
        }

        // GET: Habitaciones/Create
        public IActionResult Create()
        {
            ViewBag.Tipos = Enum.GetValues(typeof(TipoHabitacion));
            return View();
        }

        // POST: Habitaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Habitacion habitacion)
        {
            ViewBag.Tipos = Enum.GetValues(typeof(TipoHabitacion));
            if (!ModelState.IsValid)
                return View(habitacion);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(habitacion, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("api/Habitaciones", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrWhiteSpace(error) ? error : "Error al crear la habitación.");
            return View(habitacion);
        }

        // GET: Habitaciones/Edit/{numeroHabitacion}
        public async Task<IActionResult> Edit(int numeroHabitacion)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Habitaciones/{Uri.EscapeDataString(numeroHabitacion.ToString())}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var habitacion = await JsonSerializer.DeserializeAsync<Habitacion>(stream, opciones);
            if (habitacion == null) return NotFound();

            ViewBag.Tipos = Enum.GetValues(typeof(TipoHabitacion));
            return View(habitacion);
        }

        // POST: Habitaciones/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Habitacion habitacion)
        {
            ViewBag.Tipos = Enum.GetValues(typeof(TipoHabitacion));
            if (!ModelState.IsValid)
                return View(habitacion);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(habitacion, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"api/Habitaciones/{Uri.EscapeDataString(habitacion.NumeroHabitacion.ToString())}", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrWhiteSpace(error) ? error : "Error al actualizar la habitación.");
            return View(habitacion);
        }

        // GET: Habitaciones/Delete/{numeroHabitacion}
        public async Task<IActionResult> Delete(int numeroHabitacion)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Habitaciones/{Uri.EscapeDataString(numeroHabitacion.ToString())}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var habitacion = await JsonSerializer.DeserializeAsync<Habitacion>(stream, opciones);
            if (habitacion == null) return NotFound();
            return View(habitacion);
        }

        // POST: Habitaciones/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int numeroHabitacion)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.DeleteAsync($"api/Habitaciones/{Uri.EscapeDataString(numeroHabitacion.ToString())}");
            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = !string.IsNullOrWhiteSpace(error) ? error : "Error al eliminar la habitación.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Habitaciones/Buscar?numeroHabitacion=...
        public async Task<IActionResult> Buscar(int numeroHabitacion)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Habitaciones/Buscar/{Uri.EscapeDataString(numeroHabitacion.ToString())}");
            if (!resp.IsSuccessStatusCode)
                return View((Habitacion)null);

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var habitacion = await JsonSerializer.DeserializeAsync<Habitacion>(stream, opciones);
            return View(habitacion);
        }
    }
}
    

