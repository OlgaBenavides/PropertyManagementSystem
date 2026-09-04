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
    public class ReservacionesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservacionesController(IHttpClientFactory httpClientFactory)
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
                var resp = await client.GetAsync("api/Reservaciones");
                if (!resp.IsSuccessStatusCode)
                    return View(new List<Reservacion>());

                var stream = await resp.Content.ReadAsStreamAsync();
                var opciones = GetJsonOptions();
                var lista = await JsonSerializer.DeserializeAsync<List<Reservacion>>(stream, opciones);
                return View(lista ?? new List<Reservacion>());
            }
            catch
            {
                return View(new List<Reservacion>());
            }
        }

        // GET: Reservaciones/Create
        public IActionResult Create()
        {
            ViewBag.Estados = Enum.GetValues(typeof(EstadoReservacion));
            return View();
        }

        // POST: Reservaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservacion reservacion)
        {
            ViewBag.Estados = Enum.GetValues(typeof(EstadoReservacion));

            if (!ModelState.IsValid)
                return View(reservacion);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(reservacion, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("api/Reservaciones", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var respContent = await resp.Content.ReadAsStringAsync();
            try
            {
                var doc = JsonSerializer.Deserialize<Dictionary<string, object>>(respContent, opciones);
                if (doc != null && doc.ContainsKey("message"))
                {
                    ModelState.AddModelError(string.Empty, doc["message"].ToString());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, respContent);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, respContent);
            }

            return View(reservacion);
        }

        // GET: Reservaciones/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Reservaciones/{Uri.EscapeDataString(id)}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var reservacion = await JsonSerializer.DeserializeAsync<Reservacion>(stream, opciones);
            if (reservacion == null) return NotFound();

            ViewBag.Estados = Enum.GetValues(typeof(EstadoReservacion));
            return View(reservacion);
        }

        // POST: Reservaciones/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reservacion reservacion)
        {
            ViewBag.Estados = Enum.GetValues(typeof(EstadoReservacion));

            if (!ModelState.IsValid)
                return View(reservacion);

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var opciones = GetJsonOptions();
            var json = JsonSerializer.Serialize(reservacion, opciones);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PutAsync($"api/Reservaciones/{Uri.EscapeDataString(reservacion.ID)}", content);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var respContent = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, respContent);
            return View(reservacion);
        }

        // GET: Reservaciones/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.GetAsync($"api/Reservaciones/{Uri.EscapeDataString(id)}");
            if (!resp.IsSuccessStatusCode)
                return NotFound();

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();
            var reservacion = await JsonSerializer.DeserializeAsync<Reservacion>(stream, opciones);
            if (reservacion == null) return NotFound();
            return View(reservacion);
        }

        // POST: Reservaciones/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var resp = await client.DeleteAsync($"api/Reservaciones/{Uri.EscapeDataString(id)}");
            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = !string.IsNullOrWhiteSpace(error) ? error : "Error al eliminar la reservación.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservaciones/Buscar
        public async Task<IActionResult> Buscar(string id = null, string identificacionCliente = null)
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            var query = new List<string>();
            if (!string.IsNullOrWhiteSpace(id)) query.Add($"id={Uri.EscapeDataString(id)}");
            if (!string.IsNullOrWhiteSpace(identificacionCliente)) query.Add($"identificacionCliente={Uri.EscapeDataString(identificacionCliente)}");
            var url = "api/Reservaciones/Buscar";
            if (query.Count > 0) url += "?" + string.Join("&", query);

            var resp = await client.GetAsync(url);
            if (!resp.IsSuccessStatusCode)
                return View(new List<Reservacion>());

            var stream = await resp.Content.ReadAsStreamAsync();
            var opciones = GetJsonOptions();

            // Intentar deserializar a lista; si viene un solo objeto, envolverlo
            try
            {
                var lista = await JsonSerializer.DeserializeAsync<List<Reservacion>>(stream, opciones);
                return View(lista ?? new List<Reservacion>());
            }
            catch
            {
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                try
                {
                    var single = await JsonSerializer.DeserializeAsync<Reservacion>(stream, opciones);
                    return View(single != null ? new List<Reservacion> { single } : new List<Reservacion>());
                }
                catch
                {
                    return View(new List<Reservacion>());
                }
            }
        }
    

    public async Task<IActionResult> ReporteSemanal()
        {
            var client = _httpClientFactory.CreateClient("HospedajeAPI");
            try
            {
                var resp = await client.GetAsync("api/Reservaciones/ReporteSemanal");
                if (!resp.IsSuccessStatusCode)
                    return View(new List<Reservacion>());

                var stream = await resp.Content.ReadAsStreamAsync();
                var opciones = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
                };
                var lista = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Reservacion>>(stream, opciones);
                return View(lista ?? new List<Reservacion>());
            }
            catch
            {
                return View(new List<Reservacion>());
            }
        }
    }
}
