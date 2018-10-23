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
    public class Graph
    {
        public static readonly double INFINITY = double.MaxValue;

        //Colors
        private readonly Color edgeColor = Color.DarkGreen;
        private readonly Color vertexColor = Color.DarkGray;

        //They use Map and HashMap in Java
        public Dictionary<string, Vertex> vertexMap { get; set; }
        private World gameWorld;
        public List<string> keys = new List<string>();

        public Graph(World gameWorld, int distanceVertex)
        {
            vertexMap = new Dictionary<string, Vertex>();
            this.gameWorld = gameWorld;
            GenerateGraph(distanceVertex);
        }

        public void AddVertex(string name, Vector2D pos)
        {
            vertexMap[name] = new Vertex(name, pos) { drawIt = false, color = edgeColor };
        }

        public bool AddEdge(string source, string destination)
        {
            if (!vertexMap.ContainsKey(source) || !vertexMap.ContainsKey(destination))
                return false;

            double cost;
            Vertex sourceVertex = vertexMap[source];
            Vertex destinationVertex = vertexMap[destination];
            cost = CalculateCost(sourceVertex, destinationVertex);

            sourceVertex.adj.Add(new Edge(destinationVertex, cost));

            return true;
        }

        public double CalculateCost(Vertex source, Vertex destination)
        {
            double diffrenceX = Math.Abs(source.position.X - destination.position.X);
            double diffrenceY = Math.Abs(source.position.Y - destination.position.Y);

            return Math.Sqrt(diffrenceX * diffrenceX + diffrenceY * diffrenceY);
        }

        private string positionToString(double posX, double posY)
        {
            return "pos: " + posX + "-" + posY;
        }
        public void GenerateGraph(int distanceVertrex)
        {
            int offSetX = 5;
            int offSetY = 4;
            double vecX, vecY;
            string source, destination;
            int yRows = gameWorld.Height / distanceVertrex;
            int xCollumns = gameWorld.Width / distanceVertrex;
            
            //Create all Vertexs
            for(int y = 0; y < yRows; y++)
            {
                for(int x = 0; x < xCollumns; x++)
                {
                    vecX = x * distanceVertrex + offSetX;
                    vecY = y * distanceVertrex + offSetY;

                    //Create unique name for the vertex.
                    source = positionToString(vecX, vecY);
                    Vector2D posistion = new Vector2D(vecX, vecY);
                    if (gameWorld.CheckCollisionWithObject(posistion))
                    {
                        AddVertex(source, posistion);
                        keys.Add(source);
                    }
                }
            }
            //Create all edges
            for (int y = 0; y < yRows; y++)
            {
                for (int x = 0; x < xCollumns; x++)
                {
                    vecX = x * distanceVertrex + offSetX;
                    vecY = y * distanceVertrex + offSetY;

                    //Unique name for source.
                    source = positionToString(vecX, vecY);

                    //Set vector to destination.
                    vecX = (x + 1) * distanceVertrex + offSetX;

                    //Unique name for destination.
                    destination = positionToString(vecX, vecY);

                    // - First Horizontal
                    if (x < xCollumns - 1)
                    {
                        AddEdge(source, destination);
                        AddEdge(destination, source);
                    }

                    // | Second Vertical
                    vecX = x * distanceVertrex + offSetX;
                    vecY = (y + 1) * distanceVertrex + offSetY;
                    destination = positionToString(vecX, vecY);
                    if (y < yRows - 1)
                    {
                        AddEdge(source, destination);
                        AddEdge(destination, source);
                    }

                    // \ Third Diagonal from top left to bottem right and reverse.
                    vecX = (x + 1) * distanceVertrex + offSetX;
                    vecY = (y + 1) * distanceVertrex + offSetY;
                    destination = positionToString(vecX, vecY);
                    if (y < yRows - 1 && x < xCollumns - 1)
                    {
                        AddEdge(source, destination);
                        AddEdge(destination, source);
                    }

                    // / Fourth Diagnoal from top right to bottem left and reverse
                    vecX = (x - 1) * distanceVertrex + offSetX;
                    vecY = (y + 1) * distanceVertrex + offSetY;
                    destination = positionToString(vecX, vecY);
                    if (y < yRows - 1 && x > 0)
                    {
                        AddEdge(source, destination);
                        AddEdge(destination, source);
                    }
                }
            }

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
                v = new Vertex(name,position);
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
                    Vertex w = e.destination;

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
                    Vertex w = e.destination;
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
                    returnString += edge.destination.name + "(" + edge.cost + ") ";
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

        public Vertex AStar(Vertex source, Vertex destination)
        {
            //Create list to check and save checked.
            List<Vertex> checkedVertex = new List<Vertex>();
            List<Vertex> uncheckedVertex = new List<Vertex> { source };

            //Keep last Vertex in memory.
            Dictionary<Vertex, Vertex> lastVertex = new Dictionary<Vertex, Vertex>();

            //Cost from start to adjacent Vertex.
            Dictionary<Vertex, double> adjacentCost = new Dictionary<Vertex, double>();

            //Set al cost to max.
            foreach (KeyValuePair<string, Vertex> vertex in vertexMap)
            {
                adjacentCost[vertex.Value] = INFINITY;
            }

            //Source Vertex is always zero cost.
            adjacentCost[source] = 0;

            Dictionary<Vertex, Double> aStarCost = new Dictionary<Vertex, double>();

            foreach (KeyValuePair<string, Vertex> vertex in vertexMap)
            {
                aStarCost[vertex.Value] = INFINITY;
            }

            aStarCost[source] = CalculateCost(source, destination);

            //Only check unchecked vertex's
            while (uncheckedVertex.Count > 0)
            {
                Vertex current = uncheckedVertex[0];
                for (int i = 0; i < uncheckedVertex.Count; i++)
                {
                    // Get the vertex with the lowest cost of the current list.
                    if (aStarCost[current] > aStarCost[uncheckedVertex[i]])
                    {
                        current = uncheckedVertex[i];
                    }
                }

                //Reached destination path can be build now.
                if (current == destination)
                {
                    return buildPath(current, lastVertex);
                }

                uncheckedVertex.Remove(current);
                checkedVertex.Add(current);

                foreach (Edge adjacentEdge in current.adj)
                {
                    Vertex adjacentVertex = adjacentEdge.destination;
                    if (checkedVertex.Contains(adjacentVertex))
                    {
                        // Neighbour already checked.
                        continue;
                    }

                    // Explore a new node
                    if (!uncheckedVertex.Contains(adjacentVertex))
                    {
                        uncheckedVertex.Add(adjacentVertex);
                    }

                    // Distance from source to adjacent vertex.
                    int tempAdjacentCost = (int)(adjacentCost[current] + adjacentEdge.cost);
                    if (tempAdjacentCost >= adjacentCost[adjacentVertex])
                    {
                        // This is not a better path
                        continue;
                    }

                    // This path is currently the cheapest so we can add it to our lastVertex map to build it later
                    lastVertex[adjacentVertex] = current;
                    adjacentCost[adjacentVertex] = tempAdjacentCost;
                    aStarCost[adjacentVertex] = adjacentCost[adjacentVertex] + CalculateCost(adjacentVertex, destination);
                }
            }

            //AStar is not possible. 
            return null;
        }

        private Vertex buildPath(Vertex current, Dictionary<Vertex, Vertex> lastVertex)
        {
            Vertex definedPath = new Vertex(current.name, current.position);

            while(lastVertex.ContainsKey(current) && lastVertex[current] != null)
            {
                current = lastVertex[current];
                Vertex newVertex = new Vertex(current.name, current.position);

                //Add current cost to total cost.
                double cost = CalculateCost(newVertex, definedPath);
                newVertex.adj.Add(new Edge(definedPath, cost));
                definedPath = newVertex;
            }

            return definedPath;
        }

        public void DrawGraph(Graphics g)
        {
            foreach(KeyValuePair<string,Vertex> entry in vertexMap)
            {
                double x = entry.Value.position.X;
                double y = entry.Value.position.Y;
                List<Edge> edges = entry.Value.adj;

                g.FillEllipse(new SolidBrush(Color.FromArgb(50, 0, 204, 0)), new Rectangle((int)x -3, (int)y -3, 6, 6));

                foreach (Edge e in edges)
                {
                    double targetX = e.destination.position.X;
                    double targetY = e.destination.position.Y;
                    g.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0), 1), (float)x, (float)y, (float)targetX, (float)targetY);
                }
            }
        }
    }
}
