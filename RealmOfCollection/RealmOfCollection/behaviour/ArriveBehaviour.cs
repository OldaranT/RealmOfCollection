using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;

namespace RealmOfCollection.behaviour
{
    
    public class ArriveBehaviour : SteeringBehaviour
    {
        public Vector2D TargetPos;
        public Deceleration deceleration;
        public ArriveBehaviour(MovingEntity me, Vector2D targetPos) : base(me)
        {
            deceleration = Deceleration.normal;
            TargetPos = targetPos;
        }
        public ArriveBehaviour(MovingEntity me, Vector2D targetPos, Deceleration deceleration) : base(me)
        {
            this.TargetPos = targetPos;
            this.deceleration = deceleration;

        }

        public Vector2D Arrive()
        {
            Vector2D ToTarget = TargetPos - ME.Pos;
            float slowingradius = 100f;

            double dist = ToTarget.Length();
            float speed = ME.MaxSpeed;
            //Console.WriteLine("dist= " + dist);
            //Console.WriteLine("toTarget normalized lenght before and after: " + dist + " after: " + Vector2D.Vec2DNormalize(ToTarget).Length());
            if (dist < slowingradius)
            {
                speed = ME.MaxSpeed * (float)(dist / slowingradius);

            }

            Vector2D DesiredVelocity = Vector2D.Vec2DNormalize(ToTarget) * ME.MaxSpeed;
            ME.arriveSpeed = speed;
            return (DesiredVelocity - ME.Velocity);

            //if (dist < slowingradius)
            //{
            //    Vector2D DesiredVelocity = ToTarget.Normalize() * ME.MaxSpeed * (dist / slowingradius);
            //    //Console.WriteLine(dist/slowingradius);
            //    DesiredVelocity = DesiredVelocity - ME.Velocity;
            //    //Console.WriteLine("My velocity length: " + ME.Velocity.Length());
            //    Console.WriteLine("Desired velocity length" + DesiredVelocity.Length());
            //    return (DesiredVelocity);
            //}
            //else
            //{
            //    Vector2D DesiredVelocity = (ToTarget.Normalize() * ME.MaxSpeed);
            //    return (DesiredVelocity - ME.Velocity);
            //}

            //if (dist > 0)
            //{
            //    double DecelerationTweaker = 0.3;
            //    double speed = dist / ((double)deceleration * DecelerationTweaker);
            //    speed = Math.Min(speed, ME.MaxSpeed);
            //    Console.WriteLine(speed + " this is the speed");

            //    Vector2D DesiredVelocity = ToTarget * (speed / dist);
            //    return (DesiredVelocity - ME.Velocity);
            //}
            //return new Vector2D(0, 0);
        }

        public override Vector2D Calculate()
        {
            return Arrive();
        }
    }
}
