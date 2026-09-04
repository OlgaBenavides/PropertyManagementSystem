using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto3Data.Repositories
{
    public class EmpleadoRepository
    {
        private readonly HospedajeContext _context;

        public EmpleadoRepository(HospedajeContext context)
        {
            _context = context;
        }

        public List<Empleado> ObtenerTodos()
        {
            return _context.Empleados.ToList();
        }

        public Empleado? ObtenerPorIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return null;
            return _context.Empleados.FirstOrDefault(e => e.Identificacion == identificacion);
        }

        public void Agregar(Empleado empleado)
        {
            if (empleado == null) return;
            _context.Empleados.Add(empleado);
            _context.SaveChanges();
        }

        public void Actualizar(Empleado empleado)
        {
            if (empleado == null) return;
            _context.Empleados.Update(empleado);
            _context.SaveChanges();
        }

        public void Eliminar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return;
            var existente = _context.Empleados.FirstOrDefault(e => e.Identificacion == identificacion);
            if (existente == null) return;
            _context.Empleados.Remove(existente);
            _context.SaveChanges();
        }

        public bool Existe(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return false;
            return _context.Empleados.Any(e => e.Identificacion == identificacion);
        }
    }
}
