using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "klient_usluga",
                newName: "stan");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "klient",
                newName: "czy_kursant");

            migrationBuilder.CreateTable(
                name: "instruktor",
                columns: table => new
                {
                    id_instruktor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numer_telefonu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    czy_prowadzi_praktyke = table.Column<bool>(type: "bit", nullable: false),
                    czy_prowadzi_teorie = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instruktor", x => x.id_instruktor);
                });

            migrationBuilder.CreateTable(
                name: "promocja",
                columns: table => new
                {
                    id_promocja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa_promocja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    wartosc_promocja = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    opis_promocja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aktywna = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocja", x => x.id_promocja);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id_status);
                });

            migrationBuilder.CreateTable(
                name: "szczegoly_instruktor",
                columns: table => new
                {
                    id_szczegoly_instruktor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_instruktor = table.Column<int>(type: "int", nullable: false),
                    haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pesel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_miasto = table.Column<int>(type: "int", nullable: false),
                    id_kod_pocztowy = table.Column<int>(type: "int", nullable: false),
                    ulica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nr_domu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nr_lokalu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_szczegoly_instruktor", x => x.id_szczegoly_instruktor);
                    table.ForeignKey(
                        name: "FK_szczegoly_instruktor_instruktor_id_instruktor",
                        column: x => x.id_instruktor,
                        principalTable: "instruktor",
                        principalColumn: "id_instruktor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_szczegoly_instruktor_kod_pocztowy_id_kod_pocztowy",
                        column: x => x.id_kod_pocztowy,
                        principalTable: "kod_pocztowy",
                        principalColumn: "id_kod_pocztowy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_szczegoly_instruktor_miasto_id_miasto",
                        column: x => x.id_miasto,
                        principalTable: "miasto",
                        principalColumn: "id_miasto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usluga_promocja",
                columns: table => new
                {
                    id_usluga_promocja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usluga = table.Column<int>(type: "int", nullable: false),
                    id_promocja = table.Column<int>(type: "int", nullable: false),
                    data_przypisania = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usluga_promocja", x => x.id_usluga_promocja);
                    table.ForeignKey(
                        name: "FK_usluga_promocja_promocja_id_promocja",
                        column: x => x.id_promocja,
                        principalTable: "promocja",
                        principalColumn: "id_promocja",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usluga_promocja_usluga_id_usluga",
                        column: x => x.id_usluga,
                        principalTable: "usluga",
                        principalColumn: "id_usluga",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kursant_kurs",
                columns: table => new
                {
                    id_kursant_kurs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_klient = table.Column<int>(type: "int", nullable: false),
                    id_usluga = table.Column<int>(type: "int", nullable: false),
                    data_rozpoczecia = table.Column<DateOnly>(type: "date", nullable: false),
                    data_zakonczenia = table.Column<DateOnly>(type: "date", nullable: false),
                    id_status = table.Column<int>(type: "int", nullable: false),
                    PESEL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pkk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    badania_lekarskie = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kursant_kurs", x => x.id_kursant_kurs);
                    table.ForeignKey(
                        name: "FK_kursant_kurs_klient_id_klient",
                        column: x => x.id_klient,
                        principalTable: "klient",
                        principalColumn: "id_klient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kursant_kurs_status_id_status",
                        column: x => x.id_status,
                        principalTable: "status",
                        principalColumn: "id_status",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kursant_kurs_usluga_id_usluga",
                        column: x => x.id_usluga,
                        principalTable: "usluga",
                        principalColumn: "id_usluga",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_klient",
                table: "kursant_kurs",
                column: "id_klient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_status",
                table: "kursant_kurs",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_usluga",
                table: "kursant_kurs",
                column: "id_usluga",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_szczegoly_instruktor_id_instruktor",
                table: "szczegoly_instruktor",
                column: "id_instruktor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_szczegoly_instruktor_id_kod_pocztowy",
                table: "szczegoly_instruktor",
                column: "id_kod_pocztowy");

            migrationBuilder.CreateIndex(
                name: "IX_szczegoly_instruktor_id_miasto",
                table: "szczegoly_instruktor",
                column: "id_miasto");

            migrationBuilder.CreateIndex(
                name: "IX_usluga_promocja_id_promocja",
                table: "usluga_promocja",
                column: "id_promocja",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usluga_promocja_id_usluga",
                table: "usluga_promocja",
                column: "id_usluga",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kursant_kurs");

            migrationBuilder.DropTable(
                name: "szczegoly_instruktor");

            migrationBuilder.DropTable(
                name: "usluga_promocja");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "instruktor");

            migrationBuilder.DropTable(
                name: "promocja");

            migrationBuilder.RenameColumn(
                name: "stan",
                table: "klient_usluga",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "czy_kursant",
                table: "klient",
                newName: "status");
        }
    }
}
