using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelHub.Migrations
{
    /// <inheritdoc />
    public partial class Terceira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdmHotel",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmHotel", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Hospede",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospede", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdministradorUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotel_AdmHotel_AdministradorUserId",
                        column: x => x.AdministradorUserId,
                        principalTable: "AdmHotel",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quarto",
                columns: table => new
                {
                    QuartoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quarto", x => x.QuartoId);
                    table.ForeignKey(
                        name: "FK_Quarto_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    ComentarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospedeUserId = table.Column<int>(type: "int", nullable: true),
                    QuartoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.ComentarioId);
                    table.ForeignKey(
                        name: "FK_Comentario_Hospede_HospedeUserId",
                        column: x => x.HospedeUserId,
                        principalTable: "Hospede",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Comentario_Quarto_QuartoId",
                        column: x => x.QuartoId,
                        principalTable: "Quarto",
                        principalColumn: "QuartoId");
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    FotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    QuartoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.FotoId);
                    table.ForeignKey(
                        name: "FK_Fotos_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_Fotos_Quarto_QuartoId",
                        column: x => x.QuartoId,
                        principalTable: "Quarto",
                        principalColumn: "QuartoId");
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospedeUserId = table.Column<int>(type: "int", nullable: false),
                    QuartoId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reserva_Hospede_HospedeUserId",
                        column: x => x.HospedeUserId,
                        principalTable: "Hospede",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Quarto_QuartoId",
                        column: x => x.QuartoId,
                        principalTable: "Quarto",
                        principalColumn: "QuartoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_HospedeUserId",
                table: "Comentario",
                column: "HospedeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_QuartoId",
                table: "Comentario",
                column: "QuartoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_HotelId",
                table: "Fotos",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_QuartoId",
                table: "Fotos",
                column: "QuartoId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_AdministradorUserId",
                table: "Hotel",
                column: "AdministradorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quarto_HotelId",
                table: "Quarto",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_HospedeUserId",
                table: "Reserva",
                column: "HospedeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_HotelId",
                table: "Reserva",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_QuartoId",
                table: "Reserva",
                column: "QuartoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Hospede");

            migrationBuilder.DropTable(
                name: "Quarto");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "AdmHotel");
        }
    }
}
