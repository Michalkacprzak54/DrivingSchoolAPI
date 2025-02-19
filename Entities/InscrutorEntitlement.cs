﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DrivingSchoolAPI.Entities
{
    public class InscrutorEntitlement
    {
        [Column("id_instruktor_uprawnienie")]
        public int? IdInscrutorEntitlement { get; set; }

        [Column("id_instruktor")]
        public int? IdInstructor { get; set; }

        [Column("id_uprawnienie")]
        public int? IdEntitlement { get; set; }

        [Column("data_uprawnienie")]
        public DateOnly? DateEntitlement { get; set; }

        [JsonIgnore]
        public Instructor? Instructor { get; set; }
        public Entitlement? Entitlement { get; set; }
    }
}