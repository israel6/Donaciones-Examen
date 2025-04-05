using Microsoft.AspNetCore.Identity;
//UUID
using System.ComponentModel.DataAnnotations;

namespace Donaciones.Models
{
    public class UsuariosModel : IdentityUser
    {
        [Required]
        public string Cedula { get; set; }
    }
}