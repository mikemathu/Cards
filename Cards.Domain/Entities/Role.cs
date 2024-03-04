using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.Domain.Entities
{
    public class Role
    {
        [Column("RoleId")]
        [Key] 
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;//todo: Unique
    }
}
