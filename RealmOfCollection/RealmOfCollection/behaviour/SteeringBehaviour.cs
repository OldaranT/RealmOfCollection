using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        
        public virtual Vector2D Calculate(Vector2D target) { return null; }

        public virtual void Draw(Graphics g) { }

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
