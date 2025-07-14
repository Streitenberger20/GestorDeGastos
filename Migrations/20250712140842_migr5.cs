using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class migr5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descripciones_Rubros_RubroId",
                table: "Descripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Descripciones_DescripcionId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Rubros_RubroId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos");

            migrationBuilder.AddColumn<int>(
                name: "RubroId1",
                table: "Gastos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_RubroId1",
                table: "Gastos",
                column: "RubroId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Descripciones_Rubros_RubroId",
                table: "Descripciones",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Descripciones_DescripcionId",
                table: "Gastos",
                column: "DescripcionId",
                principalTable: "Descripciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Rubros_RubroId",
                table: "Gastos",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Rubros_RubroId1",
                table: "Gastos",
                column: "RubroId1",
                principalTable: "Rubros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descripciones_Rubros_RubroId",
                table: "Descripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Descripciones_DescripcionId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Rubros_RubroId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Rubros_RubroId1",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_RubroId1",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "RubroId1",
                table: "Gastos");

            migrationBuilder.AddForeignKey(
                name: "FK_Descripciones_Rubros_RubroId",
                table: "Descripciones",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Descripciones_DescripcionId",
                table: "Gastos",
                column: "DescripcionId",
                principalTable: "Descripciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Rubros_RubroId",
                table: "Gastos",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
