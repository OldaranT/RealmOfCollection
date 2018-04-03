using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    interface IGraph
    {
        Vertex GetVertex(String name);
        void AddEdge(String source, String dest, double cost);
        string ToString();

    }
}
