using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;
using RealmOfCollection.entity.MovingEntitys;

namespace RealmOfCollection.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        float ANGLE_CHANGE = (float)Math.PI;
        float CIRCLE_DISTANCE = 2;
        float CIRCLE_RADIUS = 5;
        float wanderAngle { get; set; }

        public WanderBehaviour() : base() { }

        public WanderBehaviour(MovingEntity me, Random r) : base(me, r)
        {
            wanderAngle = ((float)Random.NextDouble() * ANGLE_CHANGE) - (ANGLE_CHANGE * 0.5f);
        }

        public override Vector2D Calculate()
        {
            Vector2D circleCenter;
            if (!movingEntity.Velocity.isZero())
            {
                circleCenter = movingEntity.Velocity.Clone();
                circleCenter = circleCenter.Normalize();
                circleCenter = circleCenter.ScaleBy(CIRCLE_DISTANCE);
            }
            else
            {
                circleCenter = new Vector2D(1, 1);
                circleCenter = circleCenter.ScaleBy(CIRCLE_DISTANCE);
            }
            Vector2D displacement;
            if (!movingEntity.Velocity.isZero())
            {
                displacement = movingEntity.Velocity.Clone().Normalize();
                displacement = displacement.ScaleBy(CIRCLE_RADIUS);
            }
            else
            {
                displacement = new Vector2D(1, 1);
                displacement = displacement.ScaleBy(CIRCLE_RADIUS);
            }


            wanderAngle += ((float)Random.NextDouble() * ANGLE_CHANGE) - (ANGLE_CHANGE * 0.5f);

            //rotate the displacement with the angle
            displacement = Vector2D.rotate(displacement, wanderAngle);

            Vector2D DesiredVelocity = circleCenter.Add(displacement);

            DesiredVelocity = DesiredVelocity.Sub(movingEntity.Velocity);
            return DesiredVelocity;
        }
    }
}
