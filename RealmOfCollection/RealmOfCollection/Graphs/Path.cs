using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    class Path //Needed for Dijkstra method in Graph
    {
        public Vertex dest;
        public double cost;

        public Path(Vertex d, double c)
        {
            dest = d;
            cost = c;
        }

        public int CompareTo(Path rhs)
        {
            double otherCost = rhs.cost;

            return cost < otherCost ? -1 : cost > otherCost ? 1 : 0;
        }
    }
}
