﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        [Column("id_kod_pocztowy")]
        public int ClientIdZipCode { get; set; }
        [Column("id_miasto")]
        public int ClientIdCity { get; set; }
        [Column("ulica")]
        public string ClientStreet { get; set; }
        [Column("numer_domu")]
        public string ClientHouseNumber { get; set; }
        [Column("numer_lokalu")]
        public int? ClientFlatNumber { get; set; }

        public ClientLogin? ClientLogin { get; set; }
        public City? City { get; set; }
        public ZipCode? ZipCode { get; set; }
        public TraineeCourse? TraineeCourse { get; set; }
        public ICollection<ClientService>? ClientServices { get; set; }
    }
}