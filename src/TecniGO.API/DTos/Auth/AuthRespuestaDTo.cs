namespace TecniGO.API.DTos.Auth
{
    public class AuthRespuestaDTo
    {
        public string Token { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
    }
}