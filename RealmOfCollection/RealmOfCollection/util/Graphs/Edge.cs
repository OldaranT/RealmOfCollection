using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    public class Edge
    {
        public Vertex destination;
        public double cost;
        public Color color { get; set; }
        public bool drawIt { get; set; }
        public Edge(Vertex destination, double cost)
        {
            this.destination = destination;
            this.cost = cost;
        }
    }
}
