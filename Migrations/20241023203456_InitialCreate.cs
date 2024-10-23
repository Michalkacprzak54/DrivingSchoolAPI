using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kod_pocztowy",
                columns: table => new
                {
                    id_kod_pocztowy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kod_pocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kod_pocztowy", x => x.id_kod_pocztowy);
                });

            migrationBuilder.CreateTable(
                name: "miasto",
                columns: table => new
                {
                    id_miasto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_miasto", x => x.id_miasto);
                });

            migrationBuilder.CreateTable(
                name: "usluga",
                columns: table => new
                {
                    id_usluga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa_usluga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cena_netto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stawka_vat = table.Column<int>(type: "int", nullable: false),
                    typ_usluga = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usluga", x => x.id_usluga);
                });

            migrationBuilder.CreateTable(
                name: "klient",
                columns: table => new
                {
                    id_klient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_urodzenia = table.Column<DateOnly>(type: "date", nullable: false),
                    nr_telefonu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adres_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_kod_pocztowy = table.Column<int>(type: "int", nullable: false),
                    id_miasto = table.Column<int>(type: "int", nullable: false),
                    numer_domu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numer_lokalu = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_klient", x => x.id_klient);
                    table.ForeignKey(
                        name: "FK_klient_kod_pocztowy_id_kod_pocztowy",
                        column: x => x.id_kod_pocztowy,
                        principalTable: "kod_pocztowy",
                        principalColumn: "id_kod_pocztowy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_klient_miasto_id_miasto",
                        column: x => x.id_miasto,
                        principalTable: "miasto",
                        principalColumn: "id_miasto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "klient_usluga",
                columns: table => new
                {
                    id_klient_usluga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_klient = table.Column<int>(type: "int", nullable: false),
                    id_usluga = table.Column<int>(type: "int", nullable: false),
                    data_zakupu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ilosc = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_klient_usluga", x => x.id_klient_usluga);
                    table.ForeignKey(
                        name: "FK_klient_usluga_klient_id_klient",
                        column: x => x.id_klient,
                        principalTable: "klient",
                        principalColumn: "id_klient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_klient_usluga_usluga_id_usluga",
                        column: x => x.id_usluga,
                        principalTable: "usluga",
                        principalColumn: "id_usluga",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_klient_id_kod_pocztowy",
                table: "klient",
                column: "id_kod_pocztowy");

            migrationBuilder.CreateIndex(
                name: "IX_klient_id_miasto",
                table: "klient",
                column: "id_miasto");

            migrationBuilder.CreateIndex(
                name: "IX_klient_usluga_id_klient",
                table: "klient_usluga",
                column: "id_klient");

            migrationBuilder.CreateIndex(
                name: "IX_klient_usluga_id_usluga",
                table: "klient_usluga",
                column: "id_usluga");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "klient_usluga");

            migrationBuilder.DropTable(
                name: "klient");

            migrationBuilder.DropTable(
                name: "usluga");

            migrationBuilder.DropTable(
                name: "kod_pocztowy");

            migrationBuilder.DropTable(
                name: "miasto");
        }
    }
}
