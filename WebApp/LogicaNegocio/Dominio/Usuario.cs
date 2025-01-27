using LogicaNegocio.InterfacesDominio;
using ExcepcionesPropias;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.Dominio
{
    public  class Usuario : IValidable
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string PrimerNombre { get; set; }
        [MaxLength(100)]
        public string? SegundoNombre { get; set; }
        [Required, MaxLength(100)]
        public string PrimerApellido { get; set; }
        [MaxLength(100)]
        public string? SegundoApellido { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string Pasaporte { get; set; }
        [Required, MaxLength(255)]
        public string? PasswordHash { get; set; }
        [Phone]
        public string? Telefono { get; set; }
        public DateTime? FechaNac { get; set; }
        [Required, MaxLength(50)]
        public string Rol { get; set; }
        public bool? Estado { get; set; }
        public bool DebeCambiarContrasena { get; set; } = true;
        public List<GrupoDeViaje> GruposComoViajero { get; set; }
        public List<GrupoDeViaje> GruposComoCoordinador { get; set; }
        public string? PasaporteDocumentoRuta { get; set; }
        public string? VisaDocumentoRuta { get; set; }
        public string? VacunasDocumentoRuta { get; set; }
        public string? SeguroDeViajeDocumentoRuta { get; set; }
        [NotMapped]
        public IFormFile? PasaporteDocumento { get; set; }
        [NotMapped]
        public IFormFile? VisaDocumento { get; set; }
        [NotMapped]
        public IFormFile? VacunasDocumento { get; set; }
        [NotMapped]
        public IFormFile? SeguroDeViajeDocumento { get; set; }
        public List<Actividad>? Actividades { get; set; } = new List<Actividad>();

        public void EstablecerContrasena(string contrasena)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(contrasena);
        }

        public bool VerificarContrasena(string contrasena)
        {
            return BCrypt.Net.BCrypt.Verify(contrasena, PasswordHash);
        }

        public void ConfirmarCambioContrasena()
        {
            DebeCambiarContrasena = false;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(PrimerNombre)|| string.IsNullOrEmpty(PrimerApellido))
            {
                throw new UsuarioException("El primer nombre y primer apellido es requerido");
            }
            if (PasswordHash == null || PasswordHash.Length < 8)
            {
                throw new UsuarioException("La contraseña debe tener al menos 8 caracteres.");
            }
             if(!Regex.IsMatch(Pasaporte, @"^[A-Za-z]\d{6}$"))
            {
                throw new UsuarioException("El número de pasaporte debe tener una letra seguida por 6 números.");
            }

             if (FechaNac > DateTime.Now)
            {
                throw new UsuarioException("La fecha de nacimiento no puede ser en el futuro");
            }
        }

      
    }
}