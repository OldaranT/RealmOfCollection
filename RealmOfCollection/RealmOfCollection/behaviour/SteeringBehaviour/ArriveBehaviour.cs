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

        public ArriveBehaviour() : base()
        {

        }

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
            Vector2D ToTarget = TargetPos - movingEntity.Pos;
            float slowingradius = 100f;

            double dist = TargetPos.Distance(movingEntity.Pos);
            float speed = movingEntity.MaxSpeed;
            if (dist < slowingradius)
            {
                speed = movingEntity.MaxSpeed * (float)(dist / slowingradius);

            }

            Vector2D DesiredVelocity = Vector2D.Vec2DNormalize(ToTarget) * movingEntity.MaxSpeed;
            movingEntity.arriveSpeed = speed;
            return (DesiredVelocity - movingEntity.Velocity);
        }

        public override Vector2D Calculate()
        {
            return Arrive();
        }
    }
}
