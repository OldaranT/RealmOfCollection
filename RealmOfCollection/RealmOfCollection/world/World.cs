using RealmOfCollection.behaviour;
using RealmOfCollection.entity;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.Graphs;
using RealmOfCollection.util;
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
        public static readonly int distanceVertex = 10;

        private List<BaseGameEntity> entities = new List<BaseGameEntity>();
        private List<StaticEntity> Objects = new List<StaticEntity>();
        private List<MovingEntity> movingEntities { get; set; }
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D CursorPos { get; set; }

        public Random random { get; set; }
        public Graph graph { get; set; }
        public Path path { get; set; }
        public bool showGraph { get; set; }
        public Hunter hunter { get; set; }

        public List<ExploreTarget> exploreTargets;
        public ExploreTarget beginning;


        public World(int w, int h)
        {
            movingEntities = new List<MovingEntity>();
            showGraph = false;
            Width = w;
            Height = h;
            random = new Random();
            CreateObjects();
            graph = new Graph(this, distanceVertex);
            populate();

            exploreTargets = new List<ExploreTarget>();
            createExploreTargets();


        }

        private void populate()
        {
            path = new Path(this);
            hunter = new Hunter(new Vector2D(50, 50), this, false);
            //hunter.SB = new PathFollowBehaviour(hunter);
            hunter.SB = new ExploreBahviour(hunter, 100f);
            movingEntities.Add(hunter);

            for (int i = 0; i < 25; i++)
            {
                int X = random.Next(0, Width - 100);
                int Y = random.Next(0, Height - 100);
                int R = random.Next(0, 255);
                int G = random.Next(0, 255);
                int B = random.Next(0, 255);
                Vehicle v = new Vehicle(new Vector2D(X, Y), this, false);
                v.VColor = Color.FromArgb(255,R,G,B);
                entities.Add(v);
            }

            Target = new Vehicle(new Vector2D(100, 60), this, true);
            Target.VColor = Color.DarkRed;
            Target.Scale = 15;
            Target.Pos = new Vector2D(200, 100);

            beginning = new ExploreTarget(100, 60);

        }

        private void CreateObjects()
        {
            //entities.Add(new SqaureObject(new Vector2D(300, 300), this, new Vector2D(50, 50)));
            for(int i = 0; i <  100; i++)
            {
                int X = random.Next(0, Width - 100);
                int Y = random.Next(0, Height - 100);
                Objects.Add(new SqaureObject(new Vector2D(X, Y), this, new Vector2D(50, 50)));
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
                movingEntity.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(movingEntity, 20, Objects, 50));
                movingEntity.SteeringBehaviors.Add(new EntityAvoidanceBehaviour(movingEntity, movingEntities));
                movingEntity.Update(timeElapsed);
            }

            foreach (MovingEntity movingEntity in movingEntities)
            {
                movingEntity.Update(timeElapsed);
            }
            Target.SteeringBehaviors = new List<SteeringBehaviour>();
            //Target.SteeringBehaviors.Add(new WanderBehaviour(Target, 2500, 50, 0.001, random));
            Target.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(Target, 20, Objects, 25));
            Target.Update(timeElapsed);


        }

        public bool CheckObstruction(Vector2D pos, float size)
        {
            throw new NotImplementedException();
            // foreach door alle static entities
            // 
        }



        public bool CheckCollisionWithObject(Vector2D pos)
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

        private void createExploreTargets()
        {
            List<string> keys = graph.keys;
            int maxIndex = keys.Count - 1;
            for (int i = 0; i < 5; i++)
            {
                string key = keys[random.Next(0, maxIndex)];
                Vector2D location = graph.vertexMap[key].position;
                ExploreTarget target = new ExploreTarget(location.X, location.Y);
                if (exploreTargets.Contains(target))
                {
                    i--;
                    continue;
                }
                exploreTargets.Add(target);
            }

        }

        public void Render(Graphics g)
        {
            Objects.ForEach(e => e.Render(g));
            if (showGraph)
            {
                graph.DrawGraph(g);
            }
            entities.ForEach(e => e.Render(g));

            movingEntities.ForEach(e => e.Render(g));

            hunter.Render(g);

            Target.Render(g);

            exploreTargets.ForEach(e => e.Render(g));

        }
    }
}
