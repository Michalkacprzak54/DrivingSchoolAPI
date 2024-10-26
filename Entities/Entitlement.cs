using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPI.Entities
{
    public class Entitlement
    {
        [Column("id_uprawnienie")]
        public int IdEntitlement {  get; set; }
        [Column("uprawnienie")]
        public string EntitlementName { get; set; }

        public ICollection<InscrutorEntitlement> InstructorEntitlements { get; set; }
    }
}
