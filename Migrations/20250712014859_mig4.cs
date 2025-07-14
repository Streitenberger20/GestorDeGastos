using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescripcionId",
                table: "Gastos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_DescripcionId",
                table: "Gastos",
                column: "DescripcionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Descripciones_DescripcionId",
                table: "Gastos",
                column: "DescripcionId",
                principalTable: "Descripciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Descripciones_DescripcionId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_DescripcionId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "DescripcionId",
                table: "Gastos");
        }
    }
}
