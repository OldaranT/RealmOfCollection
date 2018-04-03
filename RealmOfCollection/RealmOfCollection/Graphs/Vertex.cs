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
        public String name; // Vertex name
        public List<Edge> adj; // Adjacent vertices
        public double dist; // Cost
        public Vertex prev; // Previous vertex on shortest path
        public int scratch;// Extra variable used in algorithm

        public Vertex(String nm)
        {
            name = nm; adj = new List<Edge>();
            Reset();
        }

        public void Reset()
        {
            dist = Graph.INFINITY; prev = null; scratch = 0;
        }

        public override string ToString()
        {
            string tmp = null;
            foreach (Edge x in adj)
            {
                tmp += x.dest.name + "(" + x.cost + ") ";
            }
            return this.name + " -->" + " " + tmp;
        }
    }
}
