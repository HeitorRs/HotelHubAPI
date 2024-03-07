using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelHub.Migrations
{
    /// <inheritdoc />
    public partial class RolesAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Hospede_HospedeId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Quarto_QuartoId",
                table: "Comentario");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Hospede",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Comentario",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "QuartoId",
                table: "Comentario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HospedeId",
                table: "Comentario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "AdmHotel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Hospede_HospedeId",
                table: "Comentario",
                column: "HospedeId",
                principalTable: "Hospede",
                principalColumn: "HospedeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Quarto_QuartoId",
                table: "Comentario",
                column: "QuartoId",
                principalTable: "Quarto",
                principalColumn: "QuartoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Hospede_HospedeId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Quarto_QuartoId",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Hospede");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "AdmHotel");

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Comentario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuartoId",
                table: "Comentario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HospedeId",
                table: "Comentario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Hospede_HospedeId",
                table: "Comentario",
                column: "HospedeId",
                principalTable: "Hospede",
                principalColumn: "HospedeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Quarto_QuartoId",
                table: "Comentario",
                column: "QuartoId",
                principalTable: "Quarto",
                principalColumn: "QuartoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
