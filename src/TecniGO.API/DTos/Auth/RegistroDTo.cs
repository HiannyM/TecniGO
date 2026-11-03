using System.ComponentModel.DataAnnotations;

namespace TecniGO.API.DTos.Auth
{
    public class RegistroDTo
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public int RolId { get; set; } // 1 = Cliente, 2 = Técnico
    }
}