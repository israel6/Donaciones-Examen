using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Donaciones.Data.Migrations
{
    /// <inheritdoc />
    public partial class entidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiario",
                columns: table => new
                {
                    BeneficiarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiario", x => x.BeneficiarioID);
                });

            migrationBuilder.CreateTable(
                name: "Campaña",
                columns: table => new
                {
                    CampanaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Objetivo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaña", x => x.CampanaID);
                });

            migrationBuilder.CreateTable(
                name: "Donante",
                columns: table => new
                {
                    DonanteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MontoDonado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donante", x => x.DonanteID);
                });

            migrationBuilder.CreateTable(
                name: "Donaciones",
                columns: table => new
                {
                    DonacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonanteID = table.Column<int>(type: "int", nullable: true),
                    CampanaID = table.Column<int>(type: "int", nullable: true),
                    BeneficiarioID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donaciones", x => x.DonacionID);
                    table.ForeignKey(
                        name: "FK_Donaciones_Beneficiario_BeneficiarioID",
                        column: x => x.BeneficiarioID,
                        principalTable: "Beneficiario",
                        principalColumn: "BeneficiarioID");
                    table.ForeignKey(
                        name: "FK_Donaciones_Campaña_CampanaID",
                        column: x => x.CampanaID,
                        principalTable: "Campaña",
                        principalColumn: "CampanaID");
                    table.ForeignKey(
                        name: "FK_Donaciones_Donante_DonanteID",
                        column: x => x.DonanteID,
                        principalTable: "Donante",
                        principalColumn: "DonanteID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_BeneficiarioID",
                table: "Donaciones",
                column: "BeneficiarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_CampanaID",
                table: "Donaciones",
                column: "CampanaID");

            migrationBuilder.CreateIndex(
                name: "IX_Donaciones_DonanteID",
                table: "Donaciones",
                column: "DonanteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donaciones");

            migrationBuilder.DropTable(
                name: "Beneficiario");

            migrationBuilder.DropTable(
                name: "Campaña");

            migrationBuilder.DropTable(
                name: "Donante");
        }
    }
}
