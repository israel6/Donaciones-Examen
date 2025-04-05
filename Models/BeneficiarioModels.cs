using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Donaciones.Models
{
    public class BeneficiarioModels
    {
        [Key]
        [Column("BeneficiarioID")]
        public int BeneficiarioId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; } = null!;

        [InverseProperty("Beneficiario")]
        public virtual ICollection<DonacionesModels> Donaciones { get; set; } = new List<DonacionesModels>();

    }
}
