using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HahnSimBack.Entities
{
    public class ActionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId {  get; set; }

        [Required]
        public string ActionType {  get; set; }
        public string? ActionDetails { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
        public bool? Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
