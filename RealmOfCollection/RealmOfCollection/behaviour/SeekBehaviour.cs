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
            Vector2D normalizedNewPos = ME.MyWorld.Target.Pos.Sub(MyPos);
            normalizedNewPos.Normalize();
            
            Vector2D DesiredVelocity = normalizedNewPos.Multiply(ME.MaxSpeed);
            return DesiredVelocity.Sub(ME.Velocity);
        }
    }
}
