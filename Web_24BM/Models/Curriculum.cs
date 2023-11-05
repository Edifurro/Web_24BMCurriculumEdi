using System.ComponentModel.DataAnnotations;


namespace Web_24BM.Models
{
    public class Curriculum
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(50, ErrorMessage ="El campo Nombre no debe seperar los 50 caracteres")]
        public string Nombre { get; set; }
        
        [StringLength(50, ErrorMessage = "El campo Apellidos no debe seperar los 50 caracteres")]
        public string Apellidos { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El fecha de nacimiento es requerida.")]
        public DateTime FechaNacimiento { get; set; }
        
        [Required(ErrorMessage ="La direccion es un campo obligatorio")]
        public string Direccion { get; set; }
        
        [Required(ErrorMessage = "El campo objetivo es requerido")]
        public string Objetivo { get; set; }

        public IFormFile? Foto { get; set; }
        public List <DatosLaboral>? DatosLaborales { get; set;}

    }
}
