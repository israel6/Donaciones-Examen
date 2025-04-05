using Donaciones.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Donaciones.Models;

namespace Donaciones.Data;

public class ApplicationDbContext : IdentityDbContext <UsuariosModel> 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<BeneficiarioModels> Beneficiario { get; set; }
    public DbSet<CampañaModels> Campaña { get; set; }
    public DbSet<DonacionesModels> Donaciones { get; set; }
    public DbSet<DonanteModels> Donante { get; set; }

}
