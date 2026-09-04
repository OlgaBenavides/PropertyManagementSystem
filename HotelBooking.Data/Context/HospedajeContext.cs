using Microsoft.EntityFrameworkCore;
using Proyecto3Data.Models;

namespace Proyecto3Data.Context
{
    public class HospedajeContext : DbContext
    {
        public HospedajeContext(DbContextOptions<HospedajeContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
    }
}
