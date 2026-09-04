using Proyecto2API.Models;

namespace Proyecto2API.Data
{
    public static class EmpleadoRepository
    {
        private static readonly List<Empleado> empleados = new List<Empleado>();

        static EmpleadoRepository()
        {
            Agregar(new Empleado
            {
                TipoIdentificacion = TipoIdentificacionEmpleado.Cedula,
                Identificacion = "2-2222-2222",
                Nombre = "Maria",
                PrimerApellido = "Gonzalez",
                SegundoApellido = "Mora",
                FechaNacimiento = new DateTime(1985, 5, 20),
                FechaIngreso = new DateTime(2020, 3, 1),
                SalarioMensual = 500000m,
                Categoria = CategoriaEmpleado.Recepcionista,
                Provincia = "Guanacaste",
                Canton = "Liberia",
                Distrito = "Liberia",
                DireccionExacta = "100 metros norte del parque central"
            });
        }

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
        
