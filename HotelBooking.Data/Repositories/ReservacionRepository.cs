using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto3Data.Models;
using Proyecto3Data.Context;

namespace Proyecto3Data.Repositories
{
    public class ReservacionRepository
    {
        private readonly HospedajeContext _context;

        public ReservacionRepository(HospedajeContext context)
        {
            _context = context;
        }

        public List<Reservacion> ObtenerTodos()
        {
            return _context.Reservaciones.ToList();
        }

        public Reservacion? ObtenerPorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;
            return _context.Reservaciones.FirstOrDefault(r => r.ID == id);
        }

        public List<Reservacion> ObtenerPorCliente(string idCliente)
        {
            if (string.IsNullOrWhiteSpace(idCliente)) return new List<Reservacion>();
            return _context.Reservaciones.Where(r => r.IDCliente == idCliente).ToList();
        }

        public void Agregar(Reservacion reservacion)
        {
            if (reservacion == null) return;
            if (reservacion.FechaReservacion == DateTime.MinValue)
                reservacion.FechaReservacion = DateTime.Now;
            if (string.IsNullOrEmpty(reservacion.ID))
                reservacion.ID = Guid.NewGuid().ToString();

            // Calcular tarifa basada en la habitacion y dias de estadia
            var habitacion = _context.Habitaciones.FirstOrDefault(h => h.NumeroHabitacion == reservacion.IDHabitacion);
            if (habitacion != null)
            {
                int dias = (reservacion.FechaSalida - reservacion.FechaInicio).Days;
                reservacion.TarifaReservacion = habitacion.TarifaPorNoche * dias;
                decimal tarifaConDescuento = reservacion.TarifaReservacion - (reservacion.TarifaReservacion * reservacion.PorcentajeDescuento / 100);
                reservacion.MontoTotal = tarifaConDescuento + (tarifaConDescuento * 0.13m);
            }

            _context.Reservaciones.Add(reservacion);
            _context.SaveChanges();
        }

        public void Actualizar(Reservacion reservacion)
        {
            if (reservacion == null) return;
            _context.Reservaciones.Update(reservacion);
            _context.SaveChanges();
        }

        public void Eliminar(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return;
            var existente = _context.Reservaciones.FirstOrDefault(r => r.ID == id);
            if (existente == null) return;
            _context.Reservaciones.Remove(existente);
            _context.SaveChanges();
        }

        public bool Existe(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return _context.Reservaciones.Any(r => r.ID == id);
        }

        public bool ExisteTraslape(int idHabitacion, DateTime fechaInicio, DateTime fechaSalida, string? excluirId = null)
        {
            if (fechaSalida <= fechaInicio) return false;

            var query = _context.Reservaciones.Where(r => r.IDHabitacion == idHabitacion);
            if (!string.IsNullOrWhiteSpace(excluirId))
            {
                query = query.Where(r => r.ID != excluirId);
            }

            return query.Any(r => fechaInicio < r.FechaSalida && fechaSalida > r.FechaInicio);
        }

        public List<Reservacion> ObtenerReservacionesSemanaEntrante()
        {
            var today = DateTime.Today;
            int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;
            var nextMonday = today.AddDays(daysUntilNextMonday);
            var nextSunday = nextMonday.AddDays(6);

            return _context.Reservaciones
                .Where(r => r.FechaInicio.Date >= nextMonday && r.FechaInicio.Date <= nextSunday)
                .OrderByDescending(r => r.MontoTotal)
                .ToList();
        }
    }
}
