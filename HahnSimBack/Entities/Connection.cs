using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HahnSimBack.Entities
{
    public class Connection
    {
        public int Id { get; set; }

        [Required]
        public int EdgeId { get; set; }

        [Required]
        public int FirstNodeId { get; set; }

        [Required]
        public int SecondNodeId { get; set; }
    }
}