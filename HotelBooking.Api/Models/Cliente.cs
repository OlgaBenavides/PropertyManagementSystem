using System.ComponentModel.DataAnnotations;

namespace Proyecto2API.Models
{
    public enum TipoIdentificacionCliente
    {
        Cedula,
        DIMEX,
        Pasaporte
    }

    public class Cliente
    {
        [Required(ErrorMessage = "La identificación es requerida")]
        [RegularExpression(@"^\d-\d{4}-\d{4}$|^\d{12}$|^[a-zA-Z0-9]{1,50}$", 
            ErrorMessage = "La identificación debe ser: cédula (1-1111-0909), DIMEX (12 dígitos) o pasaporte (1-50 caracteres alfanuméricos)")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es requerido")]
        [Display(Name = "Tipo de Identificación")]
        public TipoIdentificacionCliente TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, MinimumLength = 3, 
            ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido")]
        [StringLength(75, MinimumLength = 3, 
            ErrorMessage = "El primer apellido debe tener entre 3 y 75 caracteres")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El segundo apellido es requerido")]
        [StringLength(75, MinimumLength = 3, 
            ErrorMessage = "El segundo apellido debe tener entre 3 y 75 caracteres")]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
