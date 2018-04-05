using System;
using System.Collections.Generic;
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
        public bool drawIt;

        public Vertex(string name, Vector2D position, bool drawIt)
        {
            this.name = name;
            this.position = position;
            this.drawIt = drawIt;
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
