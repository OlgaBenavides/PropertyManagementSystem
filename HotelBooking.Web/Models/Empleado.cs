using System.ComponentModel.DataAnnotations;

namespace Proyecto1Fundamentos.Models
{
    public enum TipoIdentificacionEmpleado
    {
        Cedula,
        DIMEX
    }

    public class Empleado
    {
        [Required(ErrorMessage = "El tipo de identificación es requerido")]
        [Display(Name = "Tipo de Identificación")]
        public TipoIdentificacionEmpleado TipoIdentificacion { get; set; }
        [Required(ErrorMessage = "La identificación es requerida")]
        [RegularExpression(@"^\d-\d{4}-\d{4}$|^\d{12}$", 
            ErrorMessage = "La identificación debe ser cédula (1-1111-0909) o DIMEX (12 dígitos)")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

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

        [Required(ErrorMessage = "El salario mensual es requerido")]
        [Range(0, 5000000, 
            ErrorMessage = "El salario mensual debe estar entre 0 y 5.000.000")]
        [Display(Name = "Salario Mensual")]
        public decimal SalarioMensual { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        [Display(Name = "Categoría")]
        public CategoriaEmpleado Categoria { get; set; }

        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Display(Name = "Cantón")]
        public string Canton { get; set; }

        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "La dirección exacta es requerida")]
        [StringLength(150, MinimumLength = 1, 
            ErrorMessage = "La dirección exacta debe tener entre 1 y 150 caracteres")]
        [Display(Name = "Dirección Exacta")]
        public string DireccionExacta { get; set; }
    }

    public enum CategoriaEmpleado
    {
        [Display(Name = "Mesero")]
        Mesero,
        [Display(Name = "Salonero")]
        Salonero,
        [Display(Name = "Lavaplatos")]
        Lavaplatos,
        [Display(Name = "Recepcionista")]
        Recepcionista,
        [Display(Name = "Administrador")]
        Administrador,
        [Display(Name = "Mantenimiento")]
        Mantenimiento,
        [Display(Name = "Cocinero")]
        Cocinero,
        [Display(Name = "Chef")]
        Chef,
        [Display(Name = "Limpieza")]
        Limpieza,
        [Display(Name = "Cocina")]
        Cocina,
        [Display(Name = "Seguridad")]
        Seguridad,
        [Display(Name = "Atención al Cliente")]
        AtencionAlCliente,
        [Display(Name = "Guía Turístico")]
        GuiaTuristico,
        [Display(Name = "Encargado de Reservaciones")]
        EncargadoDeReservaciones
    }
}
