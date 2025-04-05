using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Donaciones.Models
{
    public class DonacionesModels
    {
        [Key]
        [Column("DonacionID")]
        public int DonacionId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Monto { get; set; }

        [Column("DonanteID")]
        public int? DonanteId { get; set; }

        [Column("CampanaID")]
        public int? CampanaId { get; set; }

        [Column("BeneficiarioID")]
        public int? BeneficiarioId { get; set; }

        [ForeignKey("BeneficiarioId")]
        [InverseProperty("Donaciones")]
        public virtual BeneficiarioModels? Beneficiario { get; set; }

        [ForeignKey("CampanaId")]
        [InverseProperty("Donaciones")]
        public virtual CampañaModels? Campana { get; set; }

        [ForeignKey("DonanteId")]
        [InverseProperty("Donaciones")]
        public virtual DonanteModels? Donante { get; set; }
    }
}
