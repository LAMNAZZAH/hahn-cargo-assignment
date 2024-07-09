using HahnSimBack.Entities;

namespace HahnSimBack.Dtos
{
    public class GridDataResDto
    {
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public List<Connection> Connections { get; set; }
    }
}
