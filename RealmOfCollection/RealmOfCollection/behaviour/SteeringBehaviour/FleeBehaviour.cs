using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.behaviour
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour() : base() { }

        public FleeBehaviour(MovingEntity me) : base(me)
        {

        }

        public override Vector2D Calculate()
        {
            Vector2D MyPos = movingEntity.Pos;
            Vector2D TargetPos = movingEntity.MyWorld.player.Pos;
            float MaxSpeed = movingEntity.MaxSpeed;

            Vector2D DesiredVelocity = new Vector2D((MyPos.X -TargetPos.X), (MyPos.Y - TargetPos.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(MaxSpeed);
            DesiredVelocity = DesiredVelocity.Sub(movingEntity.Velocity);
            return DesiredVelocity;
        }
    }
}
