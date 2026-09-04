using Microsoft.AspNetCore.Mvc;
using Proyecto3Data.Repositories;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _clienteRepo;

        public ClientesController(HospedajeContext context)
        {
            _clienteRepo = new ClienteRepository(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _clienteRepo.ObtenerTodos();
            return Ok(lista);
        }

        [HttpGet("{identificacion}")]
        public IActionResult GetCliente(string identificacion)
        {
            var cliente = _clienteRepo.ObtenerPorIdentificacion(identificacion);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_clienteRepo.Existe(cliente.Identificacion))
            {
                return BadRequest(new { message = "Ya existe un cliente con esa identificación." });
            }

            _clienteRepo.Agregar(cliente);
            return CreatedAtAction(nameof(GetCliente), new { identificacion = cliente.Identificacion }, cliente);
        }

        [HttpPut("{identificacion}")]
        public IActionResult Put(string identificacion, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existente = _clienteRepo.ObtenerPorIdentificacion(identificacion);
            if (existente == null) return NotFound();

            _clienteRepo.Actualizar(cliente);
            return NoContent();
        }

        [HttpDelete("{identificacion}")]
        public IActionResult Delete(string identificacion)
        {
            if (_clienteRepo.TieneReservaciones(identificacion))
            {
                return BadRequest(new { message = "El cliente tiene reservaciones activas y no puede ser eliminado." });
            }

            _clienteRepo.Eliminar(identificacion);
            return NoContent();
        }

        [HttpGet("Buscar/{identificacion}")]
        public IActionResult Buscar(string identificacion)
        {
            var cliente = _clienteRepo.ObtenerPorIdentificacion(identificacion);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }
    }
}
