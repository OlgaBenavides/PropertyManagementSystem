using Proyecto2API.Models;

namespace Proyecto2API.Data
{
    public static class ReservacionRepository
    {
        private static readonly List<Reservacion> reservaciones = new List<Reservacion>
        {
            
            new Reservacion
            {
                IDCliente = "1-1111-1111",
                IDHabitacion = 1,                   
                FechaInicio = DateTime.Today.AddDays(7),
                FechaSalida = DateTime.Today.AddDays(10),
                PorcentajeDescuento = 10m,
                SolicitudesEspeciales = "Ninguna",
                Estado = EstadoReservacion.Reservada
            }
        };

        static ReservacionRepository()
        {
            // Calcular valores para las reservaciones de ejemplo
            foreach (var r in reservaciones)
            {
                RecalcularMontos(r);
            }
        }

        public static List<Reservacion> ObtenerTodos()
        {
            return reservaciones;
        }

        public static Reservacion ObtenerPorId(string id)
        {
            return reservaciones.FirstOrDefault(r => r.ID == id);
        }

        public static List<Reservacion> ObtenerPorCliente(string idCliente)
        {
            return reservaciones.Where(r => string.Equals(r.IDCliente, idCliente, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static void Agregar(Reservacion reservacion)
        {
            if (reservacion == null) return;

            RecalcularMontos(reservacion);
            reservaciones.Add(reservacion);
        }

        public static void Actualizar(Reservacion reservacion)
        {
            if (reservacion == null) return;
            var existente = reservaciones.FirstOrDefault(r => r.ID == reservacion.ID);
            if (existente == null) return;

            // Reemplazar campos relevantes
            existente.IDCliente = reservacion.IDCliente;
            existente.IDHabitacion = reservacion.IDHabitacion;
            existente.FechaInicio = reservacion.FechaInicio;
            existente.FechaSalida = reservacion.FechaSalida;
            existente.SolicitudesEspeciales = reservacion.SolicitudesEspeciales;
            existente.PorcentajeDescuento = reservacion.PorcentajeDescuento;
            existente.Estado = reservacion.Estado;

            // Recalcular montos
            RecalcularMontos(existente);
        }

        public static void Eliminar(string id)
        {
            var existente = reservaciones.FirstOrDefault(r => r.ID == id);
            if (existente == null) return;
            reservaciones.Remove(existente);
        }

        public static bool Existe(string id)
        {
            return reservaciones.Any(r => r.ID == id);
        }

        public static bool ExisteTraslape(int idHabitacion, DateTime fechaInicio, DateTime fechaSalida, string idExcluir = null)
        {
            // Considera traslape si los rangos se intersectan (fechaInicio inclusive, fechaSalida exclusive)
            foreach (var r in reservaciones)
            {
                if (!string.IsNullOrEmpty(idExcluir) && string.Equals(r.ID, idExcluir, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (r.IDHabitacion != idHabitacion)
                    continue;

                var aStart = r.FechaInicio.Date;
                var aEnd = r.FechaSalida.Date;
                var bStart = fechaInicio.Date;
                var bEnd = fechaSalida.Date;

                // Si aEnd <= bStart o aStart >= bEnd => no traslape
                if (aEnd <= bStart || aStart >= bEnd)
                    continue;

                return true;
            }

            return false;
        }

        public static bool TieneReservacionesPorCliente(string idCliente)
        {
            return reservaciones.Any(r => string.Equals(r.IDCliente, idCliente, StringComparison.OrdinalIgnoreCase));
        }

        public static bool TieneReservacionesPorHabitacion(int idHabitacion)
        {
            return reservaciones.Any(r => r.IDHabitacion == idHabitacion);
        }

        private static void RecalcularMontos(Reservacion r)
        {
            if (r == null) return;

            // Días de estancia
            var dias = (int)(r.FechaSalida.Date - r.FechaInicio.Date).TotalDays;
            if (dias <= 0) dias = 1;

            // Obtener tarifa por noche desde repositorio de habitaciones
            var habitacion = HabitacionRepository.ObtenerPorNumero(r.IDHabitacion);
            var tarifaNoche = habitacion?.TarifaPorNoche ?? 0m;

            r.TarifaReservacion = tarifaNoche * dias;

            // Aplicar descuento
            var descuentoFactor = (100m - (r.PorcentajeDescuento <= 0m ? 0m : r.PorcentajeDescuento)) / 100m;
            var montoConDescuento = r.TarifaReservacion * descuentoFactor;

            // Aplicar IVA 13%
            r.MontoTotal = Math.Round(montoConDescuento * 1.13m, 2);
        }
    }
}
