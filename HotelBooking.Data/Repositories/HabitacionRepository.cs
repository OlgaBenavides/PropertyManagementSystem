using Proyecto3Data.Context;
using Proyecto3Data.Models;

namespace Proyecto3Data.Repositories
{
    public class HabitacionRepository
    {
        private readonly HospedajeContext _context;

        public HabitacionRepository(HospedajeContext context)
        {
            _context = context;
        }

        public List<Habitacion> ObtenerTodos()
        {
            return _context.Habitaciones.ToList();
        }

        public Habitacion? ObtenerPorNumero(int numero)
        {
            return _context.Habitaciones.FirstOrDefault(h => h.NumeroHabitacion == numero);
        }

        public void Agregar(Habitacion habitacion)
        {
            if (habitacion == null) return;
            _context.Habitaciones.Add(habitacion);
            _context.SaveChanges();
        }

        public void Actualizar(Habitacion habitacion)
        {
            if (habitacion == null) return;
            _context.Habitaciones.Update(habitacion);
            _context.SaveChanges();
        }

        public void Eliminar(int numero)
        {
            var existente = _context.Habitaciones.FirstOrDefault(h => h.NumeroHabitacion == numero);
            if (existente == null) return;
            _context.Habitaciones.Remove(existente);
            _context.SaveChanges();
        }

        public bool Existe(int numero)
        {
            return _context.Habitaciones.Any(h => h.NumeroHabitacion == numero);
        }

        public bool TieneReservaciones(int numero)
        {
            var habitacion = _context.Habitaciones.FirstOrDefault(h => h.NumeroHabitacion == numero);
            if (habitacion == null) return false;
            return _context.Reservaciones.Any(r => r.IDHabitacion == numero);
        }
    }
}
