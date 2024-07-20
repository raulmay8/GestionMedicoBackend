using System.ComponentModel.DataAnnotations;

namespace GestionMedicoBackend.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int RoleId {  get; set; }
        public string RoleName { get; set; }
    }

    public class CreateUserDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [MaxLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres.")]
        public string Password { get; set; }

        public int? RoleId { get; set; }
    }

    public class UpdateUserDto
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [MaxLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El ID del rol es requerido.")]
        public int RoleId { get; set; }
    }
    public class RequestPasswordResetDto
    {
        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }
    }


    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "El token es requerido.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es requerida.")]
        [MinLength(4, ErrorMessage = "La nueva contraseña debe tener al menos 4 caracteres.")]
        public string NewPassword { get; set; }
    }
}
