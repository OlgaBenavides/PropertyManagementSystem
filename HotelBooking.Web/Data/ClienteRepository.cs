using System.Collections.Generic;
using System.Linq;
using Proyecto1Fundamentos.Models;

namespace Proyecto1Fundamentos.Data
{
    public static class ClienteRepository
    {
        private static readonly List<Cliente> clientes = new List<Cliente>();

        public static List<Cliente> ObtenerTodos()
        {
            return clientes;
        }

        public static Cliente ObtenerPorIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return null;
            return clientes.FirstOrDefault(c => c.Identificacion == identificacion);
        }

        public static void Agregar(Cliente cliente)
        {
            if (cliente == null) return;
            clientes.Add(cliente);
        }

        public static void Actualizar(Cliente cliente)
        {
            if (cliente == null) return;
            var existente = clientes.FirstOrDefault(c => c.Identificacion == cliente.Identificacion);
            if (existente == null) return;

            // Reemplazar propiedades
            existente.Nombre = cliente.Nombre;
            existente.PrimerApellido = cliente.PrimerApellido;
            existente.SegundoApellido = cliente.SegundoApellido;
            existente.FechaNacimiento = cliente.FechaNacimiento;
        }

        public static void Eliminar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return;
            var existente = clientes.FirstOrDefault(c => c.Identificacion == identificacion);
            if (existente == null) return;
            clientes.Remove(existente);
        }

        public static bool Existe(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return false;
            return clientes.Any(c => c.Identificacion == identificacion);
        }
    }
}
