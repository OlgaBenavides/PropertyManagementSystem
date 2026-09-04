using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto3Data.Repositories
{
    public class ClienteRepository
    {
        private readonly HospedajeContext _context;

        public ClienteRepository(HospedajeContext context)
        {
            _context = context;
        }

        public List<Cliente> ObtenerTodos()
        {
            return _context.Clientes.ToList();
        }

        public Cliente? ObtenerPorIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return null;
            return _context.Clientes.FirstOrDefault(c => c.Identificacion == identificacion);
        }

        public void Agregar(Cliente cliente)
        {
            if (cliente == null) return;
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void Actualizar(Cliente cliente)
        {
            if (cliente == null) return;
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public void Eliminar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return;
            var existente = _context.Clientes.FirstOrDefault(c => c.Identificacion == identificacion);
            if (existente == null) return;
            _context.Clientes.Remove(existente);
            _context.SaveChanges();
        }

        public bool Existe(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return false;
            return _context.Clientes.Any(c => c.Identificacion == identificacion);
        }

        public bool TieneReservaciones(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return false;
            return _context.Reservaciones.Any(r => r.IDCliente == identificacion);
        }
    }
}
