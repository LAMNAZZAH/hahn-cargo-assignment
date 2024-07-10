using System.ComponentModel.DataAnnotations;

namespace HahnSimBack.Entities
{
    public class Edge
    {
        public int Id { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
    }
}
