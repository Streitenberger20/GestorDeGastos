using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                table: "Gasto");

            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Usuario_UsuarioId",
                table: "Gasto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gasto",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "NombreDeUsuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Gasto",
                newName: "Gastos");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Usuarios",
                newName: "NombreUsuario");

            migrationBuilder.RenameColumn(
                name: "Comentario",
                table: "Gastos",
                newName: "Moneda");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Gastos",
                newName: "RubroId");

            migrationBuilder.RenameIndex(
                name: "IX_Gasto_UsuarioId",
                table: "Gastos",
                newName: "IX_Gastos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Gasto_CategoriaId",
                table: "Gastos",
                newName: "IX_Gastos_RubroId");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Importe",
                table: "Gastos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaGasto",
                table: "Gastos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rubros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRubro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Descripciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RubroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descripciones_Rubros_RubroId",
                        column: x => x.RubroId,
                        principalTable: "Rubros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolRubros",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false),
                    RubroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolRubros", x => new { x.RolId, x.RubroId });
                    table.ForeignKey(
                        name: "FK_RolRubros_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolRubros_Rubros_RubroId",
                        column: x => x.RubroId,
                        principalTable: "Rubros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Descripciones_RubroId",
                table: "Descripciones",
                column: "RubroId");

            migrationBuilder.CreateIndex(
                name: "IX_RolRubros_RubroId",
                table: "RolRubros",
                column: "RubroId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Rubros_RubroId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Usuarios_UsuarioId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Descripciones");

            migrationBuilder.DropTable(
                name: "RolRubros");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Gastos",
                newName: "Gasto");

            migrationBuilder.RenameColumn(
                name: "NombreUsuario",
                table: "Usuario",
                newName: "Rol");

            migrationBuilder.RenameColumn(
                name: "RubroId",
                table: "Gasto",
                newName: "CategoriaId");

            migrationBuilder.RenameColumn(
                name: "Moneda",
                table: "Gasto",
                newName: "Comentario");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_UsuarioId",
                table: "Gasto",
                newName: "IX_Gasto_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_RubroId",
                table: "Gasto",
                newName: "IX_Gasto_CategoriaId");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreDeUsuario",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "Importe",
                table: "Gasto",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaGasto",
                table: "Gasto",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gasto",
                table: "Gasto",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Categoria_CategoriaId",
                table: "Gasto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Usuario_UsuarioId",
                table: "Gasto",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
