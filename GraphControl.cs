using openglgraphlastversion.Models;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace openglgraphlastversion
{
    public class GraphControl
    {
        private GraphPainter _painter;
        private Label _label;
        public GraphControl(GraphPainter painter, Label label)
        {
            _painter = painter;
            _label = label;
        }
        public void Solve(string graphstr, string fromstr, string tostr, string numstr)
        {
            int to = int.Parse(tostr) - 1;
            int from = int.Parse(fromstr) - 1;
            int n = int.Parse(numstr);
            Graph graph = ParseGraph(graphstr, n);
            FordBellman fb = new FordBellman(graph);
            try
            {
                var res = fb.Solve(from, to);
                Draw(res, graph);
                var MinWeight = res.Edges.Sum(x => x.Weight);
                _label.Text = $"Длина минимального пути = {MinWeight}";
            }
            catch
            {
                _label.Text = $"Путя нема";
            }
        }

        public void DrawUnsolved(string graphstr,  string numstr)
        {
            int n = int.Parse(numstr);
            Graph graph = ParseGraph(graphstr, n);
            Draw(graph);
        }

        private Graph ParseGraph(string graph, int n)
        {
            Graph gr = new Graph();
            gr.N = n;
            foreach (var i in graph.Split('\n'))
            {
                var a = i.Split(' ');
                gr.AddEdge(int.Parse(a[0]) - 1, int.Parse(a[1]) - 1, int.Parse(a[2]));
            }
            return gr;
        }

        private void Draw(FordBellmanResult res, Graph graph)
        {
            foreach (var i in graph.Edges)
            {
                _painter.DrawLine(res.Edges.Contains(i) ? Color.Green : Color.Red, i.From, i.To, graph.N, i.Weight);
            }
            for (int i = 0; i < graph.N; i++)
            {
                _painter.DrawVertex(res.Vertexes.Contains(i) ? Color.Green : Color.Red, i, graph.N);
            }
        }

        private void Draw(Graph graph)
        {
            foreach (var i in graph.Edges)
            {
                _painter.DrawLine(Color.Red, i.From, i.To, graph.N, i.Weight);
            }
            for (int i = 0; i < graph.N; i++)
            {
                _painter.DrawVertex(Color.Red, i, graph.N);
            }
        }
    }
}
