using System.ComponentModel.DataAnnotations;

namespace HahnSimBack.Entities
{
    public class Node
    {
        [Key]
        public int NodeId { get; set; }
        [Required]
        public string Name {  get; set; }
    }
}
