using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection
{
    public abstract class SteeringBehaviour
    {
        public enum Deceleration { slow = 3, normal = 2, fast = 1 };
        public MovingEntity ME { get; set; }
        public abstract Vector2D Calculate();
        public Random random { get; set; }

        public SteeringBehaviour(MovingEntity me)
        {
            ME = me;
            random = me.MyWorld.random;
        }

        public SteeringBehaviour(MovingEntity me, Random r)
        {
            ME = me;
            random = r;
        }

        
    }

    
}
