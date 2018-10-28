using RealmOfCollection.behaviour;
using RealmOfCollection.entity;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.entity.StaticEntitys;
using RealmOfCollection.Graphs;
using RealmOfCollection.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealmOfCollection
{
    public class World
    {
        public static readonly int distanceVertex = 10;

        private List<BaseGameEntity> entities = new List<BaseGameEntity>();
        public List<StaticEntity> Objects = new List<StaticEntity>();
        private List<MovingEntity> movingEntities { get; set; }
        public Player player { get; set; }
        public Hunter hunter { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D CursorPos { get; set; }
        public static Random random { get; set; }
        public Graph graph { get; set; }
        public Path path { get; set; }
        public bool showGraph { get; set; }
        public bool showEntityInfo { get; set; }
        

        public List<ExploreTarget> exploreTargets;
        public ExploreTarget home;
        public List<TorchObject> torches;

        private int amountOfImps = 50;
        private int amountOfObjects = 100;
        private int amountOfTorches = 50;
        private int ammounOfTargets = 50;



        public World(int w, int h)
        {
            movingEntities = new List<MovingEntity>();
            exploreTargets = new List<ExploreTarget>();
            torches = new List<TorchObject>();
            random = new Random();
            showGraph = false;
            Width = w;
            Height = h;
            CreateObjects();
            graph = new Graph(this, distanceVertex);
            CreateTorches();
            populate();
            createExploreTargets();

        }

        private void populate()
        {
            path = new Path(this);

            //Hunter
            hunter = new Hunter(new Vector2D(50, 50), this, 75f);
            hunter.SteeringBehaviors = new List<SteeringBehaviour>();
            hunter.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(hunter, 1, Objects, 5));
            movingEntities.Add(hunter);

            //Player
            player = new Player(new Vector2D(100, 60), this);
            player.VColor = Color.DarkRed;
            player.Scale = 15;
            player.Pos = new Vector2D(200, 100);
            player.SteeringBehaviors = new List<SteeringBehaviour>();
            player.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(player, 20, Objects, 25));




            //Add imps
            for (int i = 0; i < amountOfImps; i++)
            {
                int X = random.Next(0, Width - 100);
                int Y = random.Next(0, Height - 100);
                int R = random.Next(0, 255);
                int G = random.Next(0, 255);
                int B = random.Next(0, 255);
                Imp imp = new Imp(new Vector2D(X, Y), this);
                imp.color = Color.FromArgb(255, R, G, B);
                imp.SteeringBehaviors = new List<SteeringBehaviour>();
                imp.SteeringBehaviors.Add(new WanderBehaviour(imp, random));
                imp.SteeringBehaviors.Add(new HideBehaviour(imp, hunter, Objects, 250));
                imp.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(imp, 20, Objects, 50));
                imp.SteeringBehaviors.Add(new EntityAvoidanceBehaviour(imp, movingEntities));
                movingEntities.Add(imp);
            }

            movingEntities.Add(player);

            home = new ExploreTarget(Width / 2, Height / 2);
            home.visited = false;

        }

        public void addTorch(Vector2D pos)
        {
            TorchObject torch = new TorchObject(pos, this, new Vector2D());
            entities.Add(torch);
        }

        public void CreateTorches()
        {

            List<string> keys = graph.keys;
            int maxIndex = keys.Count - 1;
            for (int i = 0; i < amountOfTorches; i++)
            {

                string key = keys[random.Next(0, maxIndex)];
                Vector2D location = graph.vertexMap[key].position;
                TorchObject torch = new TorchObject(location, this, new Vector2D());
                if (torches.Contains(torch))
                {
                    i--;
                    continue;
                }
                torches.Add(torch);

            }
        }

        private void CreateObjects()
        {
            for (int i = 0; i < amountOfObjects; i++)
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
            for (int i = 0; i < ammounOfTargets; i++)
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

            bool created = false;

            while (!created)
            {
                string key = keys[random.Next(0, maxIndex)];
                Vector2D loc = graph.vertexMap[key].position;
                ExploreTarget target = new ExploreTarget(loc.X, loc.Y);
                if (exploreTargets.Contains(target))
                {
                    continue;
                }
                home = target;
                created = true;
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


            player.Render(g);
            torches.ForEach(t => t.Render(g));
            exploreTargets.ForEach(e => e.Render(g, Color.Gold));
            home.Render(g, Color.Black);

            hunter.Render(g);
        }
    }
}
