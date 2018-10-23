using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;

namespace RealmOfCollection.behaviour
{
    class EvadeBehaviour : SteeringBehaviour
    {
        public EvadeBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D myPos = movingEntity.Pos;
            MovingEntity target = movingEntity.MyWorld.player;
            float maxSpeed = movingEntity.MaxSpeed;

            Vector2D Distance = target.Pos - myPos;
            int updatesAhead = (int)(Distance.Length() / maxSpeed);
            Vector2D futurePosition = target.Pos + target.Velocity * updatesAhead;

            Vector2D DesiredVelocity = new Vector2D((myPos.X - futurePosition.X), (myPos.Y - futurePosition.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(maxSpeed);
            DesiredVelocity = DesiredVelocity.Sub(movingEntity.Velocity);
            return DesiredVelocity;
        }
    }
}
