using HahnCargoAutomation.Server.Data;
using HahnCargoAutomation.Server.Infrastructure;
using HahnSimBack.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HahnSimBack.Services
{
    public class PathOptimizationServic(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        public async Task<ApiResponse<PathResult>> FindShortestPath(int start, int end)
        {
            try
            {
                var nodes = await _context.Nodes.ToListAsync();
                var edges = await _context.Edges.ToListAsync();
                var connections = await _context.Connections.ToListAsync();

                var (path, cost) = Dijkstra(nodes, edges, connections, start, end);

                if (path == null)
                {
                    return ApiResponse<PathResult>.ErrorResponse(
                        "No path found between the given nodes.",
                        HttpStatusCode.NotFound
                    );
                }

                var result = new PathResult { Path = path, TotalCost = cost };
                return ApiResponse<PathResult>.SuccessResponse(result, "Shortest path found successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<PathResult>.ErrorResponse(
                    "An error occurred while processing your request.",
                    HttpStatusCode.InternalServerError,
                    new List<string> { ex.Message }
                );
            }
        }

        private (List<int> path, int cost) Dijkstra(List<Node> nodes, List<Edge> edges, List<Connection> connections, int start, int end)
        {
            var distances = new Dictionary<int, int>();
            var previous = new Dictionary<int, int>();
            var unvisited = new HashSet<int>(nodes.Select(n => n.Id));

            foreach (var n in nodes)
            {
                distances[n.Id] = int.MaxValue;
            }
            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                int current = unvisited.OrderBy(n => distances[n]).First();
                unvisited.Remove(current);

                if (current == end)
                {
                    break;
                }

                var neighbors = connections
                    .Where(c => c.FirstNodeId == current || c.SecondNodeId == current)
                    .Select(c => c.FirstNodeId == current ? c.SecondNodeId : c.FirstNodeId);

                foreach (var neighbor in neighbors)
                {
                    var edge = connections.First(c =>
                        (c.FirstNodeId == current && c.SecondNodeId == neighbor) ||
                        (c.FirstNodeId == neighbor && c.SecondNodeId == current));

                    var cost = edges.First(e => e.Id == edge.EdgeId).Cost;
                    var totalCost = distances[current] + cost;

                    if (totalCost < distances[neighbor])
                    {
                        distances[neighbor] = totalCost;
                        previous[neighbor] = current;
                    }
                }
            }

            if (!previous.ContainsKey(end))
            {
                return (null, 0);
            }

            var path = new List<int>();
            int node = end;
            while (node != start)
            {
                path.Add(node);
                node = previous[node];
            }
            path.Add(start);
            path.Reverse();

            return (path, distances[end]);
        }
    }

    public class PathResult
    {
        public List<int> Path { get; set; }
        public int TotalCost { get; set; }
    }
}
