using Proyecto2API.Models;

namespace Proyecto2API.Data
{
    public static class HabitacionRepository
    {
        private static readonly List<Habitacion> habitaciones = new List<Habitacion>();

        static HabitacionRepository()
        {
            Agregar(new Habitacion
            {
                NumeroHabitacion = 1,
                TipoHabitacion = TipoHabitacion.StartJunior,
                TarifaPorNoche = 150m,
                TvSatelital = true,
                PendientesMantenimiento = "Ninguno"
            });
        }

        public static List<Habitacion> ObtenerTodos()
        {
            return habitaciones;
        }

        public static Habitacion ObtenerPorNumero(int numero)
        {
            return habitaciones.FirstOrDefault(h => h.NumeroHabitacion == numero);
        }

        public static void Agregar(Habitacion habitacion)
        {
            if (habitacion == null) return;
            habitaciones.Add(habitacion);
        }

        public static void Actualizar(Habitacion habitacion)
        {
            if (habitacion == null) return;
            var existente = habitaciones.FirstOrDefault(h => h.NumeroHabitacion == habitacion.NumeroHabitacion);
            if (existente == null) return;

            existente.TipoHabitacion = habitacion.TipoHabitacion;
            existente.TarifaPorNoche = habitacion.TarifaPorNoche;
            existente.TvSatelital = habitacion.TvSatelital;
            existente.PendientesMantenimiento = habitacion.PendientesMantenimiento;
        }

        public static void Eliminar(int numero)
        {
            var existente = habitaciones.FirstOrDefault(h => h.NumeroHabitacion == numero);
            if (existente == null) return;
            habitaciones.Remove(existente);
        }

        public static bool Existe(int numero)
        {
            return habitaciones.Any(h => h.NumeroHabitacion == numero);
        }
    }
}
