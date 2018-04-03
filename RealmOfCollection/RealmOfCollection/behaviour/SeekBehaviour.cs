using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.behaviour
{
    public class SeekBehaviour : SteeringBehaviour
    {
        Vector2D TargetPos;

        public SeekBehaviour(MovingEntity me) : base(me, new Random())
        {
        }

        public SeekBehaviour(MovingEntity me, Random r) : base(me, r)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D MyPos = ME.Pos;
            TargetPos = ME.MyWorld.Target.Pos;
            
            float MaxSpeed = ME.MaxSpeed;

            Vector2D DesiredVelocity = new Vector2D((TargetPos.X - MyPos.X), (TargetPos.Y - MyPos.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(MaxSpeed);
            //DesiredVelocity = DesiredVelocity.Sub(ME.Velocity);
            //Console.WriteLine("=========================");
            //Console.WriteLine("DesiredVelocity X: " + DesiredVelocity.X + " Y: " + DesiredVelocity.Y);
            //Console.WriteLine("My cord X: " + MyPos.X + " Y: " + MyPos.Y);
            double Aanligende = DesiredVelocity.X;
            double overstaande = DesiredVelocity.Y;
            double schuine = Math.Sqrt(Aanligende * Aanligende + overstaande * overstaande);

            double theAngle = Math.Cos(Aanligende / schuine);
            //Console.WriteLine("The Angle: " + theAngle);
            theAngle = theAngle * 360;
            //Console.WriteLine("The Angle * 360: " + theAngle);

            //Console.WriteLine("=========================");
            DesiredVelocity = DesiredVelocity - ME.Velocity;

            return DesiredVelocity;
        }
    }
}
