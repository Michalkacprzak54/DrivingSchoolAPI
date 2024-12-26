﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kursant_kurs_klient_id_klient",
                table: "kursant_kurs");

            migrationBuilder.DropForeignKey(
                name: "FK_kursant_kurs_usluga_id_usluga",
                table: "kursant_kurs");

            migrationBuilder.DropIndex(
                name: "IX_kursant_kurs_id_klient",
                table: "kursant_kurs");

            migrationBuilder.DropIndex(
                name: "IX_kursant_kurs_id_usluga",
                table: "kursant_kurs");

            migrationBuilder.DropColumn(
                name: "adres_email",
                table: "klient");

            migrationBuilder.RenameColumn(
                name: "id_usluga",
                table: "kursant_kurs",
                newName: "id_klient_usluga");

            migrationBuilder.RenameColumn(
                name: "haslo",
                table: "klient",
                newName: "ulica");

            migrationBuilder.AddColumn<int>(
                name: "TraineeCourseIdTraineeCourse",
                table: "usluga",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "pkk",
                table: "kursant_kurs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "data_zakonczenia",
                table: "kursant_kurs",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "uwagi",
                table: "kursant_kurs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "czy_wykorzystana",
                table: "klient_usluga",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ile_wykorzystane",
                table: "klient_usluga",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "uwagi",
                table: "klient_usluga",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TraineeCourseIdTraineeCourse",
                table: "klient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "faktura",
                columns: table => new
                {
                    id_faktura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_klient = table.Column<int>(type: "int", nullable: false),
                    numer_faktura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_wystawienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_platnosci = table.Column<DateTime>(type: "datetime2", nullable: true),
                    kwota_calkowita = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faktura", x => x.id_faktura);
                    table.ForeignKey(
                        name: "FK_faktura_klient_id_klient",
                        column: x => x.id_klient,
                        principalTable: "klient",
                        principalColumn: "id_klient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "harmonogram_praktyka",
                columns: table => new
                {
                    id_harmonogram_praktyka = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_instruktor = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    dzien_tygodnia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    godzina_rozpoczęcia = table.Column<TimeOnly>(type: "time", nullable: false),
                    godzina_zakończenia = table.Column<TimeOnly>(type: "time", nullable: false),
                    czy_aktualne = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harmonogram_praktyka", x => x.id_harmonogram_praktyka);
                });

            migrationBuilder.CreateTable(
                name: "harmonogram_wyklad",
                columns: table => new
                {
                    id_harmonogram_wyklad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_instruktor = table.Column<int>(type: "int", nullable: false),
                    grupa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    dzien_tygodnia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    godzina_rozpoczecia = table.Column<TimeOnly>(type: "time", nullable: false),
                    godzina_zakonczenia = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harmonogram_wyklad", x => x.id_harmonogram_wyklad);
                });

            migrationBuilder.CreateTable(
                name: "login_klient",
                columns: table => new
                {
                    id_login_klient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_klient = table.Column<int>(type: "int", nullable: false),
                    adres_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    haslo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_klient", x => x.id_login_klient);
                    table.ForeignKey(
                        name: "FK_login_klient_klient_id_klient",
                        column: x => x.id_klient,
                        principalTable: "klient",
                        principalColumn: "id_klient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "obecnosc_wyklad",
                columns: table => new
                {
                    id_obecnosc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_szczegoly = table.Column<int>(type: "int", nullable: false),
                    id_harmonogram_wyklad = table.Column<int>(type: "int", nullable: false),
                    data_obecnosci = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obecnosc_wyklad", x => x.id_obecnosc);
                });

            migrationBuilder.CreateTable(
                name: "praktyka",
                columns: table => new
                {
                    id_praktyka = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_harmonogram = table.Column<int>(type: "int", nullable: false),
                    id_szczegoly = table.Column<int>(type: "int", nullable: false),
                    data_rezerwacji = table.Column<DateOnly>(type: "date", nullable: false),
                    data_praktyk = table.Column<DateOnly>(type: "date", nullable: false),
                    godzina_rozpoczecia = table.Column<TimeOnly>(type: "time", nullable: false),
                    godzina_zakonczenia = table.Column<TimeOnly>(type: "time", nullable: false),
                    id_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_praktyka", x => x.id_praktyka);
                });

            migrationBuilder.CreateTable(
                name: "szczegoly_kurs",
                columns: table => new
                {
                    id_szczegoly = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_kursant_kurs = table.Column<int>(type: "int", nullable: false),
                    liczba_godzin_teoria = table.Column<double>(type: "float", nullable: false),
                    liczba_godzin_praktyka = table.Column<double>(type: "float", nullable: false),
                    egzamin_wewnetrzny = table.Column<bool>(type: "bit", nullable: false),
                    data_utworzenia = table.Column<DateOnly>(type: "date", nullable: false),
                    uwagi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_szczegoly_kurs", x => x.id_szczegoly);
                    table.ForeignKey(
                        name: "FK_szczegoly_kurs_kursant_kurs",
                        column: x => x.id_kursant_kurs,
                        principalTable: "kursant_kurs",
                        principalColumn: "id_kursant_kurs",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "uprawnienia",
                columns: table => new
                {
                    id_uprawnienie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uprawnienie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uprawnienia", x => x.id_uprawnienie);
                });

            migrationBuilder.CreateTable(
                name: "zdjecie",
                columns: table => new
                {
                    id_zdjecie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usluga = table.Column<int>(type: "int", nullable: false),
                    sciezka_zdjecie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis_alternatywny = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zdjecie", x => x.id_zdjecie);
                    table.ForeignKey(
                        name: "FK_zdjecie_usluga_id_usluga",
                        column: x => x.id_usluga,
                        principalTable: "usluga",
                        principalColumn: "id_usluga",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "platnosc",
                columns: table => new
                {
                    id_platnosc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_faktura = table.Column<int>(type: "int", nullable: false),
                    data_platnosci = table.Column<DateTime>(type: "datetime2", nullable: false),
                    kwota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    metoda_platnosci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis_platnosci = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platnosc", x => x.id_platnosc);
                    table.ForeignKey(
                        name: "FK_platnosc_faktura_id_faktura",
                        column: x => x.id_faktura,
                        principalTable: "faktura",
                        principalColumn: "id_faktura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pozycja_faktura",
                columns: table => new
                {
                    id_pozycja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_faktura = table.Column<int>(type: "int", nullable: false),
                    id_klient_usluga = table.Column<int>(type: "int", nullable: false),
                    ilosc = table.Column<int>(type: "int", nullable: false),
                    cena_jednostkowa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    kwota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stawka_vat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pozycja_faktura", x => x.id_pozycja);
                    table.ForeignKey(
                        name: "FK_pozycja_faktura_faktura_id_faktura",
                        column: x => x.id_faktura,
                        principalTable: "faktura",
                        principalColumn: "id_faktura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instruktor_uprawnienie",
                columns: table => new
                {
                    id_instruktor_uprawnienie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_instruktor = table.Column<int>(type: "int", nullable: false),
                    id_uprawnienie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instruktor_uprawnienie", x => x.id_instruktor_uprawnienie);
                    table.ForeignKey(
                        name: "FK_instruktor_uprawnienie_instruktor_id_instruktor",
                        column: x => x.id_instruktor,
                        principalTable: "instruktor",
                        principalColumn: "id_instruktor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_instruktor_uprawnienie_uprawnienia_id_uprawnienie",
                        column: x => x.id_uprawnienie,
                        principalTable: "uprawnienia",
                        principalColumn: "id_uprawnienie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usluga_TraineeCourseIdTraineeCourse",
                table: "usluga",
                column: "TraineeCourseIdTraineeCourse");

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_klient",
                table: "kursant_kurs",
                column: "id_klient");

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_klient_usluga",
                table: "kursant_kurs",
                column: "id_klient_usluga");

            migrationBuilder.CreateIndex(
                name: "IX_klient_TraineeCourseIdTraineeCourse",
                table: "klient",
                column: "TraineeCourseIdTraineeCourse");

            migrationBuilder.CreateIndex(
                name: "IX_faktura_id_klient",
                table: "faktura",
                column: "id_klient");

            migrationBuilder.CreateIndex(
                name: "IX_instruktor_uprawnienie_id_instruktor",
                table: "instruktor_uprawnienie",
                column: "id_instruktor");

            migrationBuilder.CreateIndex(
                name: "IX_instruktor_uprawnienie_id_uprawnienie",
                table: "instruktor_uprawnienie",
                column: "id_uprawnienie");

            migrationBuilder.CreateIndex(
                name: "IX_login_klient_id_klient",
                table: "login_klient",
                column: "id_klient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_platnosc_id_faktura",
                table: "platnosc",
                column: "id_faktura");

            migrationBuilder.CreateIndex(
                name: "IX_pozycja_faktura_id_faktura",
                table: "pozycja_faktura",
                column: "id_faktura");

            migrationBuilder.CreateIndex(
                name: "IX_szczegoly_kurs_id_kursant_kurs",
                table: "szczegoly_kurs",
                column: "id_kursant_kurs",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_zdjecie_id_usluga",
                table: "zdjecie",
                column: "id_usluga");

            migrationBuilder.AddForeignKey(
                name: "FK_klient_kursant_kurs_TraineeCourseIdTraineeCourse",
                table: "klient",
                column: "TraineeCourseIdTraineeCourse",
                principalTable: "kursant_kurs",
                principalColumn: "id_kursant_kurs");

            migrationBuilder.AddForeignKey(
                name: "FK_kursant_kurs_klient",
                table: "kursant_kurs",
                column: "id_klient",
                principalTable: "klient",
                principalColumn: "id_klient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_kursant_kurs_klient_usluga",
                table: "kursant_kurs",
                column: "id_klient_usluga",
                principalTable: "klient_usluga",
                principalColumn: "id_klient_usluga",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usluga_kursant_kurs_TraineeCourseIdTraineeCourse",
                table: "usluga",
                column: "TraineeCourseIdTraineeCourse",
                principalTable: "kursant_kurs",
                principalColumn: "id_kursant_kurs",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_klient_kursant_kurs_TraineeCourseIdTraineeCourse",
                table: "klient");

            migrationBuilder.DropForeignKey(
                name: "FK_kursant_kurs_klient",
                table: "kursant_kurs");

            migrationBuilder.DropForeignKey(
                name: "FK_kursant_kurs_klient_usluga",
                table: "kursant_kurs");

            migrationBuilder.DropForeignKey(
                name: "FK_usluga_kursant_kurs_TraineeCourseIdTraineeCourse",
                table: "usluga");

            migrationBuilder.DropTable(
                name: "harmonogram_praktyka");

            migrationBuilder.DropTable(
                name: "harmonogram_wyklad");

            migrationBuilder.DropTable(
                name: "instruktor_uprawnienie");

            migrationBuilder.DropTable(
                name: "login_klient");

            migrationBuilder.DropTable(
                name: "obecnosc_wyklad");

            migrationBuilder.DropTable(
                name: "platnosc");

            migrationBuilder.DropTable(
                name: "pozycja_faktura");

            migrationBuilder.DropTable(
                name: "praktyka");

            migrationBuilder.DropTable(
                name: "szczegoly_kurs");

            migrationBuilder.DropTable(
                name: "zdjecie");

            migrationBuilder.DropTable(
                name: "uprawnienia");

            migrationBuilder.DropTable(
                name: "faktura");

            migrationBuilder.DropIndex(
                name: "IX_usluga_TraineeCourseIdTraineeCourse",
                table: "usluga");

            migrationBuilder.DropIndex(
                name: "IX_kursant_kurs_id_klient",
                table: "kursant_kurs");

            migrationBuilder.DropIndex(
                name: "IX_kursant_kurs_id_klient_usluga",
                table: "kursant_kurs");

            migrationBuilder.DropIndex(
                name: "IX_klient_TraineeCourseIdTraineeCourse",
                table: "klient");

            migrationBuilder.DropColumn(
                name: "TraineeCourseIdTraineeCourse",
                table: "usluga");

            migrationBuilder.DropColumn(
                name: "uwagi",
                table: "kursant_kurs");

            migrationBuilder.DropColumn(
                name: "czy_wykorzystana",
                table: "klient_usluga");

            migrationBuilder.DropColumn(
                name: "ile_wykorzystane",
                table: "klient_usluga");

            migrationBuilder.DropColumn(
                name: "uwagi",
                table: "klient_usluga");

            migrationBuilder.DropColumn(
                name: "TraineeCourseIdTraineeCourse",
                table: "klient");

            migrationBuilder.RenameColumn(
                name: "id_klient_usluga",
                table: "kursant_kurs",
                newName: "id_usluga");

            migrationBuilder.RenameColumn(
                name: "ulica",
                table: "klient",
                newName: "haslo");

            migrationBuilder.AlterColumn<string>(
                name: "pkk",
                table: "kursant_kurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "data_zakonczenia",
                table: "kursant_kurs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adres_email",
                table: "klient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_klient",
                table: "kursant_kurs",
                column: "id_klient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_kursant_kurs_id_usluga",
                table: "kursant_kurs",
                column: "id_usluga",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_kursant_kurs_klient_id_klient",
                table: "kursant_kurs",
                column: "id_klient",
                principalTable: "klient",
                principalColumn: "id_klient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_kursant_kurs_usluga_id_usluga",
                table: "kursant_kurs",
                column: "id_usluga",
                principalTable: "usluga",
                principalColumn: "id_usluga",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
