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

        public Vertex(string name)
        {
            this.name = name;
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
