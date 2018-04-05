using RealmOfCollection.behaviour;
using RealmOfCollection.entity;
using RealmOfCollection.Graphs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection
{
    public class World
    {
        private List<BaseGameEntity> entities = new List<BaseGameEntity>();
        private List<StaticEntity> Objects = new List<StaticEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D CursorPos { get; set; }

        public Random random { get; set; }
        public Graph graph { get; set; }
        public bool showGraph { get; set; }

        public World(int w, int h)
        {
            showGraph = false;
            Width = w;
            Height = h;
            random = new Random();
            graph = new Graph();
            populate();
            CreateObjects();
            GenerateGraph();

        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(75,75), this, false);
            v.VColor = Color.Blue;
            Vehicle v2 = new Vehicle(new Vector2D(250, 100), this, false);
            v2.VColor = Color.Green;
            Vehicle v3 = new Vehicle(new Vector2D(100, 100), this, false);
            v3.VColor = Color.Pink;
            entities.Add(v);
            entities.Add(v2);
            entities.Add(v3);

            Target = new Vehicle(new Vector2D(100, 60), this, true);
            Target.VColor = Color.DarkRed;
            Target.Scale = 15;
            Target.Pos = new Vector2D(200, 100);
        }

        private void CreateObjects()
        {
            //entities.Add(new SqaureObject(new Vector2D(300, 300), this, new Vector2D(50, 50)));
            for(int i = 0; i <  70; i++)
            {
                int rand1 = random.Next(0, Width - 100);
                int rand2 = random.Next(0, Height - 100);
                Objects.Add(new SqaureObject(new Vector2D(rand1, rand2), this, new Vector2D(50, 50)));
            }
        }

        public void Update(float timeElapsed)
        {
            foreach (BaseGameEntity me in entities)
            {
                //if(me is MovingEntity)
                //{
                //    Console.WriteLine("it works");
                //}

                if (double.IsNaN(me.Pos.X))
                {
                    Console.WriteLine();
                }
                var movingEntity = me as MovingEntity;

                if (movingEntity == null)
                    continue;


                movingEntity.arriveSpeed = movingEntity.MaxSpeed;
                movingEntity.SteeringBehaviors = new List<SteeringBehaviour>();
                //movingEntity.SteeringBehaviors.Add(new SeekBehaviour(movingEntity));
                movingEntity.SteeringBehaviors.Add(new WanderBehaviour(movingEntity, 2500, 50, 0.001, random));
                //movingEntity.SteeringBehaviors.Add(new ArriveBehaviour(movingEntity, Target.Pos, SteeringBehaviour.Deceleration.slow));
                movingEntity.SteeringBehaviors.Add(new HideBehaviour(movingEntity, Target, Objects));
                //movingEntity.SteeringBehaviors.Add(new EvadeBehaviour(movingEntity));
                movingEntity.Update(timeElapsed);
            }
            //Target.SteeringBehaviors = new List<SteeringBehaviour>();
            //Target.SteeringBehaviors.Add(new WanderBehaviour(Target, 2500, 50, 0.001, random));
            //Target.Update(timeElapsed);

        }

        public bool CheckObstruction(Vector2D pos, float size)
        {
            throw new NotImplementedException();
            // foreach door alle static entities
            // 
        }

        public void GenerateGraph()
        {
            int edgeSize = 20;
            int cost = 1;
            int Rows = Height / edgeSize;
            int Collums = Width / edgeSize;
            Console.WriteLine("Height: " + Height + " Width: " + Width);

            for (int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Collums; j++)
                {
                    if (j + 1 <= Collums)
                    {
                        
                        string s = "P" + j + ":" + i;
                        string d = "P" + (j + 1) + ":" + i;
                        Vector2D PosS = new Vector2D(j * edgeSize, i * edgeSize);
                        Vector2D PosD = new Vector2D((j + 1) * edgeSize, i * edgeSize);
                        bool drawS = CheckTodraw(PosS);
                        bool drawD = CheckTodraw(PosD);
                        graph.AddEdge(s, d, cost, PosS, PosD, drawS, drawD);
                        graph.AddEdge(d, s, cost, PosD, PosS, drawD, drawS);
                    }
                    if (i + 1 <= Rows)
                    {
                        string s = "P" + j + ":" + i;
                        string d = "P" + j + ":" + (i + 1);
                        Vector2D PosS = new Vector2D(j * edgeSize, i * edgeSize);
                        Vector2D PosD = new Vector2D(j * edgeSize, (i + 1) * edgeSize);
                        bool drawS = CheckTodraw(PosS);
                        bool drawD = CheckTodraw(PosD);
                        graph.AddEdge(s, d, cost, PosS, PosD, drawS, drawD);
                        graph.AddEdge(d, s, cost, PosD, PosS, drawD, drawS);
                    }
                }
            }

            //Console.WriteLine(graph.ToString());
            
        }

        public bool CheckTodraw(Vector2D pos)
        {

            bool DrawIt = true;
            foreach (StaticEntity o in Objects)
            {
                if (o.Pos.X < pos.X && pos.X < (o.Pos.X + o.size.X) &&
                    o.Pos.Y < pos.Y && pos.Y < (o.Pos.Y + o.size.Y))
                {
                    DrawIt = false;
                }
            }
            return DrawIt;
        }

        public void Render(Graphics g)
        {
            Objects.ForEach(e => e.Render(g));
            if (showGraph)
            {
                graph.DrawGraph(g);
            }
            entities.ForEach(e => e.Render(g));

            Target.Render(g);

        }
    }
}
