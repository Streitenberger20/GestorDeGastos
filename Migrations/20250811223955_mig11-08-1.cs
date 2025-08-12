using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class mig11081 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "esActivo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "esActivo",
                table: "Usuarios");
        }
    }
}
