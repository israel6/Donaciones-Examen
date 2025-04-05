using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Donaciones.Models
{
    public class DonanteModels
    {
        [Key]
        [Column("DonanteID")]
        public int DonanteId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; } = null!;

        [StringLength(100)]
        public string CorreoElectronico { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoDonado { get; set; }

        [InverseProperty("Donante")]
        public virtual ICollection<DonacionesModels> Donaciones { get; set; } = new List<DonacionesModels>();
    }
}
