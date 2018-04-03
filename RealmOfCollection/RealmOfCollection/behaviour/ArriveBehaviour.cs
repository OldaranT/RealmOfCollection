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
        public ArriveBehaviour(MovingEntity me) : base(me)
        {
        }
        public ArriveBehaviour(MovingEntity me, Vector2D targetPos, Deceleration deceleration) : base(me)
        {
            this.TargetPos = targetPos;
            this.deceleration = deceleration;

        }

        public Vector2D Arrive(Vector2D TargetPos, Deceleration deceleration)
        {
            Vector2D ToTarget = TargetPos - ME.Pos;

            double dist = ToTarget.Length();

            if(dist > 0)
            {
                double DecelerationTweaker = 0.3;
                double speed = dist / ((double)deceleration * DecelerationTweaker);
                speed = Math.Min(speed, ME.MaxSpeed);

                Vector2D DesiredVelocity = ToTarget * (speed / dist);
                return (DesiredVelocity - ME.Velocity);
            }
            return new Vector2D(0, 0);
        }

        public override Vector2D Calculate()
        {
            return Arrive(TargetPos, deceleration);
        }
    }
}
