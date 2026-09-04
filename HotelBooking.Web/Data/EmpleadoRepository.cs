using System.Collections.Generic;
using System.Linq;
using Proyecto1Fundamentos.Models;

namespace Proyecto1Fundamentos.Data
{
    public static class EmpleadoRepository
    {
        private static readonly List<Empleado> empleados = new List<Empleado>();

        public static List<Empleado> ObtenerTodos()
        {
            return empleados;
        }

        public static Empleado ObtenerPorIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return null;
            return empleados.FirstOrDefault(e => e.Identificacion == identificacion);
        }

        public static void Agregar(Empleado empleado)
        {
            if (empleado == null) return;
            empleados.Add(empleado);
        }

        public static void Actualizar(Empleado empleado)
        {
            if (empleado == null) return;
            var existente = empleados.FirstOrDefault(e => e.Identificacion == empleado.Identificacion);
            if (existente == null) return;

            existente.Nombre = empleado.Nombre;
            existente.PrimerApellido = empleado.PrimerApellido;
            existente.SegundoApellido = empleado.SegundoApellido;
            existente.FechaNacimiento = empleado.FechaNacimiento;
            existente.SalarioMensual = empleado.SalarioMensual;
            existente.FechaIngreso = empleado.FechaIngreso;
            existente.Categoria = empleado.Categoria;
            existente.Provincia = empleado.Provincia;
            existente.Canton = empleado.Canton;
            existente.Distrito = empleado.Distrito;
            existente.DireccionExacta = empleado.DireccionExacta;
        }

        public static void Eliminar(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return;
            var existente = empleados.FirstOrDefault(e => e.Identificacion == identificacion);
            if (existente == null) return;
            empleados.Remove(existente);
        }

        public static bool Existe(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion)) return false;
            return empleados.Any(e => e.Identificacion == identificacion);
        }
    }
}
