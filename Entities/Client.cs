using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using DrivingSchoolAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolAPI.Entities
{
    public class Client
    {
        [Column("id_klient")]
        public int IdClient { get; set; }
        [Column("imie")]
        public string ClientFirstName { get; set; }
        [Column("nazwisko")]
        public string ClientLastName { get; set; }
        [Column("data_urodzenia")]
        public DateOnly ClientBirthDay { get; set; }
        [Column("nr_telefonu")]
        public string ClientPhoneNumber { get; set; }
        [Column("adres_email")]
        public string ClientEmail { get; set; }
        [Column("haslo")]
        public string ClientPassword { get; set; }
        [Column("id_kod_pocztowy")]
        public int ClientIdZipCode { get; set; }
        [Column("id_miasto")]
        public int ClientIdCity { get; set; }
        [Column("numer_domu")]
        public string ClientHouseNumber { get; set; }
        [Column("numer_lokalu")]
        public int? ClientFlatNumber { get; set; }
        [Column("status")]
        public bool ClientStatus { get; set; }

        public City City { get; set; }
        public ZipCode ZipCode { get; set; }
    }
}