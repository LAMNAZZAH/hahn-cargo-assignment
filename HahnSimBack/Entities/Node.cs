using System.ComponentModel.DataAnnotations;

namespace HahnSimBack.Entities
{
    public class Node
    {
        public int Id { get; set; }
        [Required]
        public string Name {  get; set; }
    }
}
