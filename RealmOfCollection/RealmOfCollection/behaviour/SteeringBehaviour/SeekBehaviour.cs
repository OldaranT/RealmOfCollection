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

        public SeekBehaviour() : base() { }

        public SeekBehaviour(MovingEntity me) : base(me, new Random()) { }

        public SeekBehaviour(MovingEntity me, Random r) : base(me, r) { }

        public override Vector2D Calculate()
        {
            Vector2D MyPos = movingEntity.Pos;
            TargetPos = movingEntity.MyWorld.player.Pos;
            
            float MaxSpeed = movingEntity.MaxSpeed;

            Vector2D DesiredVelocity = new Vector2D((TargetPos.X - MyPos.X), (TargetPos.Y - MyPos.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(MaxSpeed);
            DesiredVelocity = DesiredVelocity.Sub(movingEntity.Velocity);
            
            return DesiredVelocity;
        }

        public override Vector2D Calculate(Vector2D target)
        {
            Vector2D targetPos = target.Clone();
            Vector2D desired = targetPos.Sub(movingEntity.Pos);

            desired.Normalize();
            desired.Multiply(movingEntity.MaxSpeed);

            Vector2D force = desired.Sub(movingEntity.Velocity);
            force.Truncate(movingEntity.Max_Force);

            return force;
        }
    }
}
