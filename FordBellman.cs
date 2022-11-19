using openglgraphlastversion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openglgraphlastversion
{
    public class FordBellman
    {
        private Graph _graph;
        public FordBellman(Graph graph)
        {
            _graph = graph;
        }
        public FordBellmanResult Solve(int from, int to)
        {
            FordBellmanResult res = new FordBellmanResult();
            var disntances = new List<Vert>();
            for(int i = 0;i<_graph.N;i++)
            {
                disntances.Add(new Vert());
            }
            disntances[from].Dist = 0;

            for (int i = 0; i < _graph.N; i++)
            {
                for (int j = 0; j<_graph.Edges.Count;j++)
                { 
                    var edge = _graph.Edges[j];
                    if (disntances[edge.To].Dist > disntances[edge.From].Dist+edge.Weight )
                    {
                        disntances[edge.To].Dist = disntances[edge.From].Dist + edge.Weight;
                        disntances[edge.To].EdgeNumber = j;
                    }
                }
            }
            if (disntances[to].EdgeNumber == -1)
            {
                throw new Exception($"Путь до вершины {to} не найден");
            }
            var target = to;
            while (true)
            {
                if (disntances[target].EdgeNumber != -1)
                {
                    var edge = _graph.Edges[disntances[target].EdgeNumber];
                    res.Edges.Add(edge);
                    res.Vertexes.Add(target);
                    res.Vertexes.Add(edge.From);
                    target = edge.From;

                }
                else
                {
                    break;
                }
            }
            return res;
        }
    }

    public class FordBellmanResult
    {
        public HashSet<Edge> Edges { get; set; } = new HashSet<Edge>();
        public HashSet<int> Vertexes { get; set; } = new HashSet<int>();
    }

    public class Vert
    {
        public int Dist { get; set; } = int.MaxValue/2;
        public int EdgeNumber { get; set; } = -1;
    }
}