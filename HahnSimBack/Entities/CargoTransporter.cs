using System.ComponentModel.DataAnnotations;

namespace HahnSimBack.Entities
{
    public class CargoTransporter
    {
        [Key]
        public int TransporterId {  get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
