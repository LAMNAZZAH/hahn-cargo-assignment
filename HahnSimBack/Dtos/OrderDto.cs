using System.Text.Json.Serialization;

namespace HahnSimBack.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int OriginNodeId { get; set; }
        public int TargetNodeId { get; set; }
        public int Load { get; set; }
        public int Value { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DeliveryDateUtc { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ExpirationDateUtc { get; set; }
    }
}
