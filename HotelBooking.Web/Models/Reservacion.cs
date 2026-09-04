using System.ComponentModel.DataAnnotations;

namespace Proyecto1Fundamentos.Models
{
    public class Reservacion
    {
        public Reservacion()
        {
            ID = Guid.NewGuid().ToString();
        }

        [Display(Name = "ID Reservación")]
        public string? ID { get; set; }

        [Required(ErrorMessage = "La identificación del cliente es requerida")]
        [Display(Name = "Identificación del Cliente")]
        public string IDCliente { get; set; }

        [Required(ErrorMessage = "El número de habitación es requerido")]
        [Display(Name = "Número de Habitación")]
        public int IDHabitacion { get; set; }

        [Display(Name = "Fecha de Reservación")]
        public DateTime FechaReservacion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de salida es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Salida")]
        public DateTime FechaSalida { get; set; }

        [Display(Name = "Tarifa de Reservación")]
        public decimal TarifaReservacion { get; set; }

        [StringLength(300, ErrorMessage = "Las solicitudes especiales no pueden superar 300 caracteres")]
        [Display(Name = "Solicitudes Especiales")]
        public string SolicitudesEspeciales { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje de descuento debe estar entre 0 y 100")]
        [Display(Name = "Porcentaje de Descuento")]
        public decimal PorcentajeDescuento { get; set; }

        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [Display(Name = "Estado")]
        public EstadoReservacion Estado { get; set; }
    }

    public enum EstadoReservacion
    {
        [Display(Name = "Reservada")]
        Reservada,
        [Display(Name = "Cancelada")]
        Cancelada,
        [Display(Name = "En Proceso")]
        EnProceso,
        [Display(Name = "Finalizada")]
        Finalizada
    }
}
