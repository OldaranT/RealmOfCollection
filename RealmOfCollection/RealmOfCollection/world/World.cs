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
        public Player player { get; set; }
        public Hunter hunter { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D CursorPos { get; set; }
        public Random random { get; set; }
        public Graph graph { get; set; }
        public Path path { get; set; }
        public bool showGraph { get; set; }
        public bool showEntityInfo { get; set; }

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
            hunter = new Hunter(new Vector2D(50, 50), this);
            hunter.SteeringBehaviors = new List<SteeringBehaviour>();
            hunter.SteeringBehaviors.Add(new ExploreBahviour(hunter, 100f));
            hunter.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(hunter, 1, Objects, 5));
            //hunter.SB = new PathFollowBehaviour(hunter);
            //hunter.SB = new ExploreBahviour(hunter, 100f);
            //hunter.setCollisionAvoidance(new CollisionAvoidanceBehaviour(hunter, 1, Objects, 5));
            movingEntities.Add(hunter);

            //Player
            player = new Player(new Vector2D(100, 60), this);
            player.VColor = Color.DarkRed;
            player.Scale = 15;
            player.Pos = new Vector2D(200, 100);
            player.SteeringBehaviors = new List<SteeringBehaviour>();
            player.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(player, 20, Objects, 25));


            //Add imps
            for (int i = 0; i < 25; i++)
            {
                int X = random.Next(0, Width - 100);
                int Y = random.Next(0, Height - 100);
                int R = random.Next(0, 255);
                int G = random.Next(0, 255);
                int B = random.Next(0, 255);
                Imp imp = new Imp(new Vector2D(X, Y), this);
                imp.color = Color.FromArgb(255,R,G,B);
                imp.SteeringBehaviors = new List<SteeringBehaviour>();
                imp.SteeringBehaviors.Add(new WanderBehaviour(imp, 2500, 50, 0.001, random));
                imp.SteeringBehaviors.Add(new HideBehaviour(imp, player, Objects, 150));
                imp.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(imp, 20, Objects, 50));
                imp.SteeringBehaviors.Add(new EntityAvoidanceBehaviour(imp, movingEntities));
                movingEntities.Add(imp);
            }
           
            movingEntities.Add(player);

            beginning = new ExploreTarget(Width/2, Height/2);
            beginning.visited = false;

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
           

            foreach (MovingEntity movingEntity in movingEntities)
            {
                movingEntity.Update(timeElapsed);
                
            }


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

            player.Render(g);

            exploreTargets.ForEach(e => e.Render(g, Color.Gold));
            beginning.Render(g, Color.Black);

        }
    }
}
