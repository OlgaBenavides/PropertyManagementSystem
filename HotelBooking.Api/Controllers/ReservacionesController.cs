using Microsoft.AspNetCore.Mvc;
using Proyecto3Data.Repositories;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase
    {
        private readonly ReservacionRepository _reservacionRepo;
        private readonly ClienteRepository _clienteRepo;
        private readonly HabitacionRepository _habitacionRepo;

        public ReservacionesController(HospedajeContext context)
        {
            _reservacionRepo = new ReservacionRepository(context);
            _clienteRepo = new ClienteRepository(context);
            _habitacionRepo = new HabitacionRepository(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _reservacionRepo.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservacion(string id)
        {
            var r = _reservacionRepo.ObtenerPorId(id);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reservacion reservacion)
        {
            if (reservacion == null) return BadRequest(new { message = "Reservación inválida." });

            var cliente = _clienteRepo.ObtenerPorIdentificacion(reservacion.IDCliente);
            if (cliente == null)
                return BadRequest(new { message = "El cliente especificado no existe." });

            var habit = _habitacionRepo.ObtenerPorNumero(reservacion.IDHabitacion);
            if (habit == null)
                return BadRequest(new { message = "La habitación especificada no existe." });

            if (reservacion.FechaSalida <= reservacion.FechaInicio)
                return BadRequest(new { message = "La fecha de salida debe ser posterior a la fecha de inicio." });

            if (_reservacionRepo.ExisteTraslape(reservacion.IDHabitacion, reservacion.FechaInicio, reservacion.FechaSalida))
                return BadRequest(new { message = "La habitación tiene una reservación que traslapa las fechas solicitadas." });

            _reservacionRepo.Agregar(reservacion);
            return CreatedAtAction(nameof(GetReservacion), new { id = reservacion.ID }, reservacion);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Reservacion reservacion)
        {
            if (reservacion == null) return BadRequest(new { message = "Reservación inválida." });

            if (reservacion.FechaSalida <= reservacion.FechaInicio)
                return BadRequest(new { message = "La fecha de salida debe ser posterior a la fecha de inicio." });

            var existente = _reservacionRepo.ObtenerPorId(id);
            if (existente == null) return NotFound();

            if (_reservacionRepo.ExisteTraslape(reservacion.IDHabitacion, reservacion.FechaInicio, reservacion.FechaSalida, id))
                return BadRequest(new { message = "La habitación tiene una reservación que traslapa las fechas solicitadas." });

            reservacion.ID = id;
            _reservacionRepo.Actualizar(reservacion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existente = _reservacionRepo.ObtenerPorId(id);
            if (existente == null) return NotFound();

            _reservacionRepo.Eliminar(id);
            return NoContent();
        }

        [HttpGet("Buscar")]
        public IActionResult Buscar([FromQuery] string codigoId = null, [FromQuery] string identificacionCliente = null)
        {
            if (!string.IsNullOrWhiteSpace(codigoId))
            {
                var r = _reservacionRepo.ObtenerPorId(codigoId);
                var list = r != null ? new List<Reservacion> { r } : new List<Reservacion>();
                return Ok(list);
            }

            if (!string.IsNullOrWhiteSpace(identificacionCliente))
            {
                var lista = _reservacionRepo.ObtenerPorCliente(identificacionCliente);
                return Ok(lista);
            }

            var all = _reservacionRepo.ObtenerTodos();
            return Ok(all);
        }

        [HttpGet("ReporteSemanal")]
        public IActionResult ReporteSemanal()
        {
            var lista = _reservacionRepo.ObtenerReservacionesSemanaEntrante();
            return Ok(lista);
        }
    }
}
