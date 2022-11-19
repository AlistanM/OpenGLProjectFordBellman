using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openglgraphlastversion.Models
{
    public class Graph
    {
        public int N { get; set; }
        public List<Edge> Edges { get; set; } = new List<Edge>();

        public void AddEdge(int from, int to, int weight) 
        { 
            Edge edge = new Edge 
            { 
                From = from,
                To = to,
                Weight = weight
                
            };
            Edges.Add(edge);
        }

    }
}
