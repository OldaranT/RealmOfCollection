using RealmOfCollection.entity;
using RealmOfCollection.Graphs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    public class Graph : IGraph
    {
        public static readonly double INFINITY = double.MaxValue;

        //They use Map and HashMap in Java
        private Dictionary<string, Vertex> vertexMap = new Dictionary<string, Vertex>();

        public void AddEdge(string source, string dest, double cost,
            Vector2D sourcePos, Vector2D destPos, bool sourceDrawIt, bool destDrawIt)
        {
            bool drawIt = false;
            Vertex v = GetVertex(source,sourcePos, sourceDrawIt);
            Vertex w = GetVertex(dest, destPos, destDrawIt);
            if (destDrawIt && sourceDrawIt)
                drawIt = true;
            v.adj.Add(new Edge(w, cost, drawIt));
        }

        public void PrintPath(string destName)
        {
            Vertex w;
            vertexMap.TryGetValue(destName, out w);

            if (w == null)
                throw new Exception();
            else if (w.dist == INFINITY)
                Console.WriteLine("{0} is unreachable", destName);
            else
            {
                Console.Write("(Cost is: {0}) ", w.dist);
                PrintPath(w);
                Console.WriteLine();
            }
        }

        public Vertex GetVertex(string name, Vector2D position, bool drawIt)
        {
            Vertex v;
            vertexMap.TryGetValue(name, out v);

            if (v == null)
            {
                v = new Vertex(name,position, drawIt);
                vertexMap.Add(name, v);
            }

            return v;
        }

        // Exercise 2
        public void Unweigthed(string startName)
        {
            ClearAll();

            Vertex start;
            vertexMap.TryGetValue(startName, out start);

            if (start == null)
                throw new KeyNotFoundException();

            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(start);
            start.dist = 0;

            while (q.Count != 0)
            {
                Vertex v = q.Dequeue();

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;

                    if (w.dist == INFINITY)
                    {
                        w.dist = v.dist + 1;
                        w.prev = v;
                        q.Enqueue(w);
                    }
                }
            }
        }

        // Exercise 3
        public void Dijkstra(string startName)
        {
            PathPriorityQueue pq = new PathPriorityQueue();

            Vertex start;
            vertexMap.TryGetValue(startName, out start);

            if (start == null)
                throw new Exception("Start vertex no found");

            ClearAll();
            pq.Add(new Path(start, 0));
            start.dist = 0;

            int nodesSeen = 0;
            while (!pq.IsEmpty() && nodesSeen < vertexMap.Count)
            {
                Path vrec = pq.Remove();
                Vertex v = vrec.dest;

                if (v.scratch != 0) // Already processed
                    continue;

                v.scratch = 1;
                nodesSeen++;

                foreach (Edge e in v.adj)
                {
                    Vertex w = e.dest;
                    double cvw = e.cost;

                    if (cvw < 0)
                        throw new Exception();

                    if (w.dist > v.dist + cvw)
                    {
                        w.dist = v.dist + cvw;
                        w.prev = v;
                        pq.Add(new Path(w, w.dist));
                    }
                }
            }
        }

        public bool IsConnected() // Excersize 4
        {

            bool connected = true;
            foreach (KeyValuePair<string, Vertex> entry in vertexMap)
            {
                //Dijkstra(entry.Key);
                Unweigthed(entry.Key);

                if (!AllPathsReachable())
                {
                    connected = false;
                    break;
                }
            }

            if (connected)
                Console.WriteLine("Graph is connected!");
            else
                Console.WriteLine("Graph is disconnected!");

            return connected;
        }

        private bool AllPathsReachable()// needed for Excersize 4
        {
            foreach (KeyValuePair<string, Vertex> entry in vertexMap)
            {
                if (entry.Value == null)
                    throw new Exception();
                else if (entry.Value.dist == INFINITY)
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            string returnString = "";

            //Loop through Dictionary (vertexMap)
            foreach (KeyValuePair<string, Vertex> entry in vertexMap.OrderBy(p => p.Key))
            {
                if (entry.Value.dist != INFINITY)
                    returnString += entry.Key + "(" + entry.Value.dist + ") --> ";
                else
                    returnString += entry.Key + " --> ";

                //Loop through edges (adj)
                foreach (Edge edge in entry.Value.adj)
                {
                    returnString += edge.dest.name + "(" + edge.cost + ") ";
                }

                returnString += "\n";
            }

            return returnString;
        }

        private void ClearAll()
        {
            foreach (KeyValuePair<string, Vertex> entry in vertexMap)
                entry.Value.Reset();
        }

        private void PrintPath(Vertex dest)
        {
            if (dest.prev != null)
            {
                PrintPath(dest.prev);
                Console.Write(" to ");
            }
            Console.WriteLine(dest.name);
        }

        public void DrawGraph(Graphics g)
        {
            foreach(KeyValuePair<string,Vertex> entry in vertexMap)
            {
                double x = entry.Value.position.X;
                double y = entry.Value.position.Y;
                List<Edge> edges = entry.Value.adj;
                if (!entry.Value.drawIt)
                    continue;

                g.FillEllipse(new SolidBrush(Color.FromArgb(50, 0, 204, 0)), new Rectangle((int)x -5, (int)y -5, 10, 10));

                foreach (Edge e in edges)
                {
                    if (!e.drawIt)
                        continue;
                    double targetX = e.dest.position.X;
                    double targetY = e.dest.position.Y;
                    g.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0), 1), (float)x, (float)y, (float)targetX, (float)targetY);
                }
            }
        }
    }
}
