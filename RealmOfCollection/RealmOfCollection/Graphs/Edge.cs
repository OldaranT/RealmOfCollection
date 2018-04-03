using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    public class Edge
    {
        public Vertex dest;
        public double cost;

        public Edge(Vertex d, double c) {
             dest = d;
             cost = c;
        }
}
}
