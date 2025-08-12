using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class mig11082 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "esActivo",
                table: "Rubros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "esActivo",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "esActivo",
                table: "Gastos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "esActivo",
                table: "Detalles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "esActivo",
                table: "Rubros");

            migrationBuilder.DropColumn(
                name: "esActivo",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "esActivo",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "esActivo",
                table: "Detalles");
        }
    }
}
