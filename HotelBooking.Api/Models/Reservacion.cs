using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2API.Models
{
    public enum EstadoReservacion
    {
        Reservada,
        Cancelada,
        EnProceso,
        Finalizada
    }

    public class Reservacion
    {
        public Reservacion()
        {
            ID = Guid.NewGuid().ToString();
            FechaReservacion = DateTime.Now;
            TarifaReservacion = 0m;
            PorcentajeDescuento = 0m;
            MontoTotal = 0m;
            Estado = EstadoReservacion.Reservada;
        }

        [Display(Name = "ID")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "El ID del cliente es requerido")]
        [Display(Name = "ID Cliente")]
        public string IDCliente { get; set; }

        [Required(ErrorMessage = "El ID de la habitación es requerido")]
        [Display(Name = "ID Habitación")]
        public int IDHabitacion { get; set; }

        [Display(Name = "Fecha de Reservación")]
        [DataType(DataType.DateTime)]
        public DateTime FechaReservacion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de salida es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Salida")]
        public DateTime FechaSalida { get; set; }

        [Display(Name = "Tarifa Reservación")]
        public decimal TarifaReservacion { get; set; }

        [MaxLength(300, ErrorMessage = "Las solicitudes especiales no pueden exceder 300 caracteres")]
        [Display(Name = "Solicitudes Especiales")]
        public string SolicitudesEspeciales { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje de descuento debe estar entre 0 y 100")]
        [Display(Name = "Porcentaje Descuento")]
        public decimal PorcentajeDescuento { get; set; }

        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }

        [Required(ErrorMessage = "El estado de la reservación es requerido")]
        [Display(Name = "Estado Reservación")]
        public EstadoReservacion Estado { get; set; }
    }
}
