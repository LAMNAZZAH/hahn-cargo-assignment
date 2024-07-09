namespace HahnSimBack.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int OriginNodeId { get; set; }
        public int TargetNodeId { get; set; }
        public int Load { get; set; }
        public int Value { get; set; }
        public DateTime DeliveryDateUtc { get; set; }
        public DateTime ExpirationDateUtc { get; set; }
    }
}
