using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    public class Path 
    {
        public Vertex bestPath;
        private World world;
        private Graph graph;
        //const int edgeSize = 8;


        public Vertex dest;
        public double cost;

        public Path(Vertex d, double c)
        {
            dest = d;
            cost = c;
        }

        public Path( World world)
        {
            this.world = world;
            graph = world.graph;
        }

        public int CompareTo(Path rhs)
        {
            double otherCost = rhs.cost;

            return cost < otherCost ? -1 : cost > otherCost ? 1 : 0;
        }

        public Vertex FindBestPath(string source, string destination)
        {
            bestPath = graph.AStar(graph.vertexMap[source], graph.vertexMap[destination]);
            return bestPath;
        }

        public string getNearestVertex(Vector2D pos)
        {
            // "pos: " + vecX + "-" + vecY;

            int posX = (int)pos.X;
            int posY = (int)pos.Y;

            string prefix = "pos: ";
            string suffix = "-";

            Dictionary<string, Vertex> vertexMap = graph.vertexMap;

            for(int i = 0; i < World.distanceVertex * 2; i++)
            {
                string posXMin = prefix + (posX - i) + suffix;
                string posXMax = prefix + (posX + i) + suffix;

                for(int j = 0; j < World.distanceVertex * 2; j++)
                {
                    string posMin = posXMin + (posY - j);
                    string posMax = posXMax + (posY + j);

                    if (vertexMap.ContainsKey(posMin))
                    {
                        return posMin;
                    }
                    if (vertexMap.ContainsKey(posMax))
                    {
                        return posMax;
                    }
                }
            }
            return "notfound";
        }

        public void Render(Graphics g)
        {
            PointF pos;
            PointF destPos;
            Vertex dest = null;

            if(bestPath == null)
            {
                return;
            }
            Vertex currVertex = bestPath;
            while(currVertex != null)
            {
                pos = new PointF((float)currVertex.position.X, (float)currVertex.position.Y);
                foreach(Edge edge in currVertex.adj)
                {
                    dest = edge.destination;
                    pos = new PointF(pos.X, pos.Y);
                    destPos = new PointF((float)dest.position.X, (float)dest.position.Y);
                    g.DrawLine(new Pen(Color.Cyan, 1), pos, destPos);
                }
                pos = new PointF((float)currVertex.position.X - 1.1f, (float)currVertex.position.Y - 1.1f);
                g.FillEllipse(Brushes.Cyan, pos.X, pos.Y, 3, 3);

                if(currVertex.adj.Count == 0)
                {
                    break;
                }

                currVertex = dest;
            }
        }
    }
}
