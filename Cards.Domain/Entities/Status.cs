using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.Domain.Entities
{
    public class Status
    {
        [Column("StatusId")]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = "Todo";
    }
}
