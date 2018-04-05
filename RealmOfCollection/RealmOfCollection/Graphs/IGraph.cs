using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    interface IGraph
    {
        Vertex GetVertex(String name, Vector2D position, bool drawIt);
        void AddEdge(String source, String dest, double cost, Vector2D sourcePos, Vector2D destPos, bool sourceDrawIt, bool destDrawIt);
        string ToString();
    }
}
