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
        public Hunter(Vector2D pos, World world, bool player) : base(pos, world, player)
        {
            Scale = 10;
            MaxSpeed = 2;
            Max_Force = 0.3f;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D force = SB.Calculate();

            SteeringForce = force.Clone().divide(Mass);
            
            Velocity.Add(SteeringForce.Clone().Multiply(timeElapsed));

            Velocity.Truncate(MaxSpeed);

            Pos.Add(Velocity.Clone().Multiply(timeElapsed));

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Clone().Normalize();
                Side = Heading.Perp();
            }
           
           
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            float size = Scale * 2;

            Pen p = new Pen(Color.Red, 10);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));

            SB?.Draw(g);
        }
    }
}
