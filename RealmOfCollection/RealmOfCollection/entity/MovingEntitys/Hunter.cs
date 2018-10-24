using RealmOfCollection.behaviour;
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
        public static readonly double STAMINA_LIMIT = 20;
        public double tinder { get; set; }
        public static readonly double TINDERBOX_CAPACITY = 40;
        public static double TINDER_USAGE = 10;
        public Brain brain { get; set; }

        public Hunter(Vector2D pos, World world) : base(pos, world)
        {
            stamina = STAMINA_LIMIT;
            tinder = TINDERBOX_CAPACITY;
            Scale = 10;
            radius = 10;
            MaxSpeed = 100;
            Max_Force = 25f;
            brain = new Brain(this);
        }

        public override void Update(float timeElapsed)
        {
            brain.Process();
            base.Update(timeElapsed);

        }

        public void setCollisionAvoidance(SteeringBehaviour collisionAvoidance)
        {
            this.collisionAvoidance = collisionAvoidance;
        }

        //public override void Update(float timeElapsed)
        //{
            
            //Vector2D force = collisionAvoidance.Calculate();
            //force += SB.Calculate(); 
            ////force = Vector2D.truncate(force, Max_Force);
            //if (force != null)
            //{

            //    SteeringForce = force.Clone().divide(Mass);

            //    Velocity.Add(SteeringForce.Clone().Multiply(timeElapsed));

            //    Velocity.Truncate(MaxSpeed);
            //}
            //else
            //{
            //    force = new Vector2D();
            //    Velocity = new Vector2D();
            //}


            //Pos.Add(Velocity.Clone().Multiply(timeElapsed));

            //if (Velocity.LengthSquared() > 0.00000001)
            //{
            //    Heading = Velocity.Clone().Normalize();
            //    Side = Heading.PerpRightHand();
            //}
            
            //treat the screen as a toroid
            //Vector2D.WrapAround(this.Pos, MyWorld.Width, MyWorld.Height);


        //}

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            float size = Scale * 2;

            Pen p = new Pen(Color.Red, 10);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            g.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)Pos.X - 5, (int)Pos.Y - 5, 10, 10));
            foreach(SteeringBehaviour sb in SteeringBehaviors)
            {
                sb.Draw(g);
            }
            //SB?.Draw(g);
        }
    }
}
