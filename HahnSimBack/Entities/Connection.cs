using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HahnSimBack.Entities
{
    public class Connection
    {
        [Key]
        public int ConnectionId { get; set; }

        [Required]
        public int EdgeId { get; set; }

        [Required]
        public int FirstNodeId { get; set; }

        [Required]
        public int SecondNodeId { get; set; }
    }
}