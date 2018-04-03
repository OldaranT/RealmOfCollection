using RealmOfCollection.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    public class Graph : IGraph
    {
        public static double INFINITY = Double.MaxValue;
        private Dictionary<String, Vertex> vertexMap = new Dictionary<string, Vertex>();


        public void AddEdge(String sourceName, String destName, double cost)
        {
            Vertex v = GetVertex(sourceName);
            Vertex w = GetVertex(destName);
            v.adj.Add(new Edge(w, cost));

        }

        public Vertex GetVertex(String vertexName)
        {
            Vertex v;
            if (vertexMap.ContainsKey(vertexName))
            {
                v = vertexMap[vertexName];
            } 
            else
            {
                v = new Vertex(vertexName);
                vertexMap.Add(vertexName, v);
            }
            return v;
        }

        public override string ToString()
        {
            string tmp = null;
            foreach(Vertex x in vertexMap.Values)
            {
                tmp +=x.ToString() + "\n";
            }
            return tmp;
        }
    }
}
