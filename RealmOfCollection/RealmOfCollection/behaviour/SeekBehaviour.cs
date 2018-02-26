using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D MyPos = ME.Pos;
            Vector2D TargetPos = ME.MyWorld.Target.Pos;
            float MaxSpeed = ME.MaxSpeed;

            Vector2D DesiredVelocity = new Vector2D((TargetPos.X - MyPos.X), (TargetPos.Y - MyPos.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(MaxSpeed);
            DesiredVelocity = DesiredVelocity.Sub(ME.Velocity);
            return DesiredVelocity;
        }
    }
}
