using RealmOfCollection.behaviour;
using RealmOfCollection.entity.StaticEntitys;
using RealmOfCollection.FuzzyLogic;
using RealmOfCollection.Goals.CompositeGoals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity.MovingEntitys
{
    public class Hunter : MovingEntity
    {
        public SteeringBehaviour collisionAvoidance;

        public double stamina { get; set; }
        public static readonly double STAMINA_LIMIT = 100;
        public double tinder { get; set; }
        public static readonly double TINDERBOX_CAPACITY = 50;
        public static double TINDER_USAGE = 10;
        public Brain brain { get; set; }
        public float searchRadius { get; set; }
        public List<TorchObject> foundTorches;
        public double level;

        protected FuzzyModule fuzzyModule;

        public Hunter(Vector2D pos, World world, float searchRadius) : base(pos, world)
        {
            stamina = STAMINA_LIMIT;
            tinder = 0;
            Scale = 10;
            radius = 10;
            MaxSpeed = 100;
            Max_Force = 25f;
            foundTorches = new List<TorchObject>();
            this.searchRadius = searchRadius;
            brain = new Brain(this);
            this.searchRadius = searchRadius;
            foundTorches = new List<TorchObject>();
            fuzzyModule = new FuzzyModule();
            level = 0;
            FuzzyInitializer.InitializeRules(this);
        }

        public override void Update(float timeElapsed)
        {
            MyWorld.torches.ForEach(t =>
            {
                if (!t.onFire && !foundTorches.Contains(t) && t.Pos.Distance(Pos) <= searchRadius)
                {
                    foundTorches.Add(t);
                }
            });

            brain.Process();
            base.Update(timeElapsed);

        }

        public void setCollisionAvoidance(SteeringBehaviour collisionAvoidance)
        {
            this.collisionAvoidance = collisionAvoidance;
        }

        public ref FuzzyModule GetFuzzyModule()
        {
            return ref fuzzyModule;
        }

        public bool FoundUnIgnitedTorchInWorld()
        {
            bool UnIgnited = false;
            foreach (TorchObject torchObject in MyWorld.torches)
            {
                if (!torchObject.onFire)
                {
                    UnIgnited = true;
                }
            }

            return UnIgnited;
        }

        public bool FoundUnIgnitedTorchThatAreFound()
        {
            bool UnIgnited = false;
            foreach (TorchObject torchObject in foundTorches)
            {
                if (!torchObject.onFire)
                {
                    UnIgnited = true;
                }
            }

            return UnIgnited;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            float size = Scale * 2;

            Pen p = new Pen(Color.Red, 10);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            if (MyWorld.showEntityInfo)
            {
                brain.Render(g);
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)Pos.X - 5, (int)Pos.Y - 5, 10, 10));
                List<SteeringBehaviour> clonedList = new List<SteeringBehaviour>();
                clonedList.AddRange(SteeringBehaviors);
                try
                {
                    foreach (SteeringBehaviour sb in SteeringBehaviors)
                    {   
                        sb?.Draw(g);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
