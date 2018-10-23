using RealmOfCollection.behaviour;
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
        public Hunter(Vector2D pos, World world) : base(pos, world)
        {
            Scale = 10;
            radius = 10;
            MaxSpeed = 2;
            Max_Force = 25f;
        }
        public void setCollisionAvoidance(SteeringBehaviour collisionAvoidance)
        {
            this.collisionAvoidance = collisionAvoidance;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D force = SB.Calculate();
            force += collisionAvoidance.Calculate();
            force = Vector2D.truncate(force, Max_Force);
            if (force != null)
            {

                SteeringForce = force.Clone().divide(Mass);

                Velocity.Add(SteeringForce.Clone().Multiply(timeElapsed));

                Velocity.Truncate(MaxSpeed);
            }
            else
            {
                force = new Vector2D();
                Velocity = new Vector2D();
            }


            Pos.Add(Velocity.Clone().Multiply(timeElapsed));

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Clone().Normalize();
                Side = Heading.PerpRightHand();
            }
            
            //treat the screen as a toroid
            //Vector2D.WrapAround(this.Pos, MyWorld.Width, MyWorld.Height);


        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            float size = Scale * 2;

            Pen p = new Pen(Color.Red, 10);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            g.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)Pos.X - 5, (int)Pos.Y - 5, 10, 10));

            SB?.Draw(g);
        }
    }
}
