using Microsoft.AspNetCore.Mvc;
using Proyecto3Data.Repositories;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoRepository _empleadoRepo;

        public EmpleadosController(HospedajeContext context)
        {
            _empleadoRepo = new EmpleadoRepository(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _empleadoRepo.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{identificacion}")]
        public IActionResult GetEmpleado(string identificacion)
        {
            var empleado = _empleadoRepo.ObtenerPorIdentificacion(identificacion);
            if (empleado == null) return NotFound();
            return Ok(empleado);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_empleadoRepo.Existe(empleado.Identificacion))
            {
                return BadRequest(new { message = "Ya existe un empleado con esa identificación." });
            }

            _empleadoRepo.Agregar(empleado);
            return CreatedAtAction(nameof(GetEmpleado), new { identificacion = empleado.Identificacion }, empleado);
        }

        [HttpPut("{identificacion}")]
        public IActionResult Put(string identificacion, [FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existente = _empleadoRepo.ObtenerPorIdentificacion(identificacion);
            if (existente == null) return NotFound();

            _empleadoRepo.Actualizar(empleado);
            return NoContent();
        }

        [HttpDelete("{identificacion}")]
        public IActionResult Delete(string identificacion)
        {
            _empleadoRepo.Eliminar(identificacion);
            return NoContent();
        }

        [HttpGet("Buscar/{identificacion}")]
        public IActionResult Buscar(string identificacion)
        {
            var empleado = _empleadoRepo.ObtenerPorIdentificacion(identificacion);
            if (empleado == null) return NotFound();
            return Ok(empleado);
        }
    }
}
