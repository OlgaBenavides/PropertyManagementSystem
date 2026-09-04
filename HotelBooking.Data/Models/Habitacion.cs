using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto3Data.Models
{
    public class Habitacion
    {
        [Required(ErrorMessage = "El número de habitación es requerido")]
        [Range(1, 500,
            ErrorMessage = "El número de habitación debe estar entre 1 y 500")]
        [Display(Name = "Número de Habitación")]
        [Key]
        public int NumeroHabitacion { get; set; }

        [Required(ErrorMessage = "El tipo de habitación es requerido")]
        [Display(Name = "Tipo de Habitación")]
        public string TipoHabitacion { get; set; }

        [Required(ErrorMessage = "La tarifa por noche es requerida")]
        [Range(50, 800,
            ErrorMessage = "La tarifa por noche debe estar entre 50 y 800")]
        [Display(Name = "Tarifa por Noche")]
        public decimal TarifaPorNoche { get; set; }

        [Display(Name = "TV Satelital")]
        public bool TvSatelital { get; set; }

        [StringLength(500,
            ErrorMessage = "Los pendientes de mantenimiento no pueden exceder 500 caracteres")]
        [Display(Name = "Pendientes de Mantenimiento")]
        public string PendientesMantenimiento { get; set; }
    }

    public enum TipoHabitacion
    {
        [Display(Name = "Start Junior")]
        StartJunior,
        [Display(Name = "Start Vista al Mar")]
        StartVistaAlMar,
        [Display(Name = "Master Start")]
        MasterStart
    }
}

