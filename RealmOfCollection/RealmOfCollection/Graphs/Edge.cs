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
        public Vertex dest;
        public double cost;
        public bool drawIt;

        public Edge(Vertex destination, double cost, bool drawIt)
        {
            dest = destination;
            this.cost = cost;
            this.drawIt = drawIt;
        }
    }
}
