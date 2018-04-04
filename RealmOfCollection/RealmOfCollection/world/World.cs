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

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            random = new Random();
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
            Objects.Add(new SqaureObject(new Vector2D(300, 300), this, new Vector2D(50, 50)));
            Objects.Add(new SqaureObject(new Vector2D(150, 200), this, new Vector2D(50, 50)));
            Objects.Add(new SqaureObject(new Vector2D(250, 150), this, new Vector2D(50, 50)));
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
            Target.SteeringBehaviors = new List<SteeringBehaviour>();
            Target.SteeringBehaviors.Add(new WanderBehaviour(Target, 2500, 50, 0.001, random));
            Target.Update(timeElapsed);

        }

        public bool CheckObstruction(Vector2D pos, float size)
        {
            throw new NotImplementedException();
            // foreach door alle static entities
            // 
        }

        public void GenerateGraph()
        {
            Console.WriteLine("============== Exercise 1 ==============");
            Graph graph = new Graph();
            graph.AddEdge("V0", "V1", 2);
            graph.AddEdge("V0", "V3", 1);
            graph.AddEdge("V1", "V3", 3);
            graph.AddEdge("V1", "V4", 10);
            graph.AddEdge("V2", "V0", 4);
            graph.AddEdge("V2", "V5", 5);
            graph.AddEdge("V3", "V2", 2);
            graph.AddEdge("V3", "V4", 2);
            graph.AddEdge("V3", "V5", 8);
            graph.AddEdge("V3", "V6", 4);
            graph.AddEdge("V4", "V6", 6);
            graph.AddEdge("V6", "V5", 1);

            Console.WriteLine(graph.ToString());

            Console.WriteLine("============== Exercise 2 ==============\n");
            graph.Unweigthed("V0");
            graph.PrintPath("V6");
            graph.PrintPath("V2");
            Console.WriteLine(graph.ToString());

            Console.WriteLine("============== Exercise 3 ==============\n");
            graph.Dijkstra("V0");
            graph.PrintPath("V6");
            graph.PrintPath("V5");
            graph.PrintPath("V4");
            graph.PrintPath("V2");

            Console.WriteLine("============== Exercise 4 ==============\n");
            graph.IsConnected();

            Graph graph2 = new Graph();
            graph2.AddEdge("V0", "V1", 3);
            graph2.AddEdge("V1", "V2", 3);
            graph2.AddEdge("V2", "V0", 3);
            graph2.IsConnected();
            
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Objects.ForEach(e => e.Render(g));

            Target.Render(g);
        }
    }
}
