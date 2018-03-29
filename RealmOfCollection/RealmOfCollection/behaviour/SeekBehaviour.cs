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
        public bool wander { get; set; }
        Vector2D TargetPos;

        public SeekBehaviour(MovingEntity me, bool wander) : base(me)
        {
            this.wander = wander;

        }

        public override Vector2D Calculate()
        {
            Vector2D MyPos = ME.Pos;
            if (wander)
            {
                TargetPos = wanderT();
            }
            else
            {
                TargetPos = ME.MyWorld.Target.Pos;
            }
            float MaxSpeed = ME.MaxSpeed;

            Vector2D DesiredVelocity = new Vector2D((TargetPos.X - MyPos.X), (TargetPos.Y - MyPos.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(MaxSpeed);
            DesiredVelocity = DesiredVelocity.Sub(ME.Velocity);
            return DesiredVelocity;
        }
    }
}
