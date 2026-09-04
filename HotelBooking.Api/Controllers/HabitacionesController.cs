using Microsoft.AspNetCore.Mvc;
using Proyecto3Data.Repositories;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionesController : ControllerBase
    {
        private readonly HabitacionRepository _habitacionRepo;

        public HabitacionesController(HospedajeContext context)
        {
            _habitacionRepo = new HabitacionRepository(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _habitacionRepo.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{numeroHabitacion}")]
        public IActionResult GetHabitacion(int numeroHabitacion)
        {
            var habitacion = _habitacionRepo.ObtenerPorNumero(numeroHabitacion);
            if (habitacion == null) return NotFound();
            return Ok(habitacion);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Habitacion habitacion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_habitacionRepo.Existe(habitacion.NumeroHabitacion))
            {
                return BadRequest(new { message = "Ya existe una habitación con ese número." });
            }

            _habitacionRepo.Agregar(habitacion);
            return CreatedAtAction(nameof(GetHabitacion), new { numeroHabitacion = habitacion.NumeroHabitacion }, habitacion);
        }

        [HttpPut("{numeroHabitacion}")]
        public IActionResult Put(int numeroHabitacion, [FromBody] Habitacion habitacion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existente = _habitacionRepo.ObtenerPorNumero(numeroHabitacion);
            if (existente == null) return NotFound();

            _habitacionRepo.Actualizar(habitacion);
            return NoContent();
        }

        [HttpDelete("{numeroHabitacion}")]
        public IActionResult Delete(int numeroHabitacion)
        {
            if (_habitacionRepo.TieneReservaciones(numeroHabitacion))
            {
                return BadRequest(new { message = "La habitación tiene reservaciones y no puede ser eliminada." });
            }

            _habitacionRepo.Eliminar(numeroHabitacion);
            return NoContent();
        }

        [HttpGet("Buscar/{numeroHabitacion}")]
        public IActionResult Buscar(int numeroHabitacion)
        {
            var habitacion = _habitacionRepo.ObtenerPorNumero(numeroHabitacion);
            if (habitacion == null) return NotFound();
            return Ok(habitacion);
        }
    }
}
