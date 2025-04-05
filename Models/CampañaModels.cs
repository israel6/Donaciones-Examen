using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Donaciones.Models
{
    public class CampañaModels
    {
        [Key]
        [Column("CampanaID")]
        public int CampanaId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Objetivo { get; set; }

        [InverseProperty("Campana")]
        public virtual ICollection<DonacionesModels> Donaciones { get; set; } = new List<DonacionesModels>();

    }
}
