using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DrivingSchoolAPI.Entities
{
    public class Entitlement
    {
        [Column("id_uprawnienie")]
        public int IdEntitlement { get; set; }

        [Column("uprawnienie")]
        public string EntitlementName { get; set; }

        [JsonIgnore]
        public ICollection<InscrutorEntitlement> InstructorEntitlements { get; set; }
    }
}