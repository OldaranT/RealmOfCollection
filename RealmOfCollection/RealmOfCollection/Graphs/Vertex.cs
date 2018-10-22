using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    // Represents a vertex in the graph.
    public class Vertex : IVertex
    {
        public string name;
        public List<Edge> adj;
        public double dist;
        public Vertex prev;
        public int scratch;
        public Vector2D position;
        public bool drawIt { get; set; }
        public Color color { get; set; }

        public Vertex(string name, Vector2D position)
        {
            this.name = name;
            this.position = position;
            adj = new List<Edge>();
            Reset();
        }

        public void Reset()
        {
            dist = Graph.INFINITY;
            prev = null;
            scratch = 0;
        }
    }
}
