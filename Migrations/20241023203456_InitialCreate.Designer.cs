﻿// <auto-generated />
using System;
using DrivingSchoolAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DrivingSchoolAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241023203456_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrivingSchoolAPI.Entities.City", b =>
                {
                    b.Property<int>("IdCity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_miasto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCity"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nazwa");

                    b.HasKey("IdCity");

                    b.ToTable("miasto", (string)null);
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_klient");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<DateOnly>("ClientBirthDay")
                        .HasColumnType("date")
                        .HasColumnName("data_urodzenia");

                    b.Property<string>("ClientEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("adres_email");

                    b.Property<string>("ClientFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("imie");

                    b.Property<int?>("ClientFlatNumber")
                        .HasColumnType("int")
                        .HasColumnName("numer_lokalu");

                    b.Property<string>("ClientHouseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("numer_domu");

                    b.Property<int>("ClientIdCity")
                        .HasColumnType("int")
                        .HasColumnName("id_miasto");

                    b.Property<int>("ClientIdZipCode")
                        .HasColumnType("int")
                        .HasColumnName("id_kod_pocztowy");

                    b.Property<string>("ClientLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nazwisko");

                    b.Property<string>("ClientPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("haslo");

                    b.Property<string>("ClientPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nr_telefonu");

                    b.Property<bool>("ClientStatus")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("IdClient");

                    b.HasIndex("ClientIdCity");

                    b.HasIndex("ClientIdZipCode");

                    b.ToTable("klient", (string)null);
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.ClientService", b =>
                {
                    b.Property<int>("IdClientService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_klient_usluga");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClientService"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("id_klient");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_zakupu");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("ilosc");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int")
                        .HasColumnName("id_usluga");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.HasKey("IdClientService");

                    b.HasIndex("ClientId");

                    b.HasIndex("ServiceId");

                    b.ToTable("klient_usluga", (string)null);
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.Service", b =>
                {
                    b.Property<int>("IdService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_usluga");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdService"));

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("opis");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nazwa_usluga");

                    b.Property<decimal>("ServiceNetPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("cena_netto");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("typ_usluga");

                    b.Property<int>("ServiceVatRate")
                        .HasColumnType("int")
                        .HasColumnName("stawka_vat");

                    b.HasKey("IdService");

                    b.ToTable("usluga", (string)null);
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.ZipCode", b =>
                {
                    b.Property<int>("IdZipCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_kod_pocztowy");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdZipCode"));

                    b.Property<string>("ZipCodeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("kod_pocztowy");

                    b.HasKey("IdZipCode");

                    b.ToTable("kod_pocztowy", (string)null);
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.Client", b =>
                {
                    b.HasOne("DrivingSchoolAPI.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("ClientIdCity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrivingSchoolAPI.Entities.ZipCode", "ZipCode")
                        .WithMany()
                        .HasForeignKey("ClientIdZipCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("ZipCode");
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.ClientService", b =>
                {
                    b.HasOne("DrivingSchoolAPI.Entities.Client", "Client")
                        .WithMany("ClientServices")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrivingSchoolAPI.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("DrivingSchoolAPI.Entities.Client", b =>
                {
                    b.Navigation("ClientServices");
                });
#pragma warning restore 612, 618
        }
    }
}
