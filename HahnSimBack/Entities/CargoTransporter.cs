using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HahnSimBack.Entities
{
    public class CargoTransporter
    {
        [Key]
        public int TransporterId {  get; set; }
        public string PathString { get; set; }
        public int PathCost { get; set; }
        public TimeSpan PathTime { get; set; }

        [NotMapped]
        public List<int> Path
        {
            get => PathString?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
            set => PathString = string.Join(",", value);
        }
    }
}
