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
        public MovingEntity movingEntity { get; set; }
        public abstract Vector2D Calculate();

        public virtual Vector2D Calculate(Vector2D target) { return new Vector2D(); }

        public virtual void Draw(Graphics g) { }

        public Random random { get; set; }

        public SteeringBehaviour()
        {

        }

        public SteeringBehaviour(MovingEntity me)
        {
            movingEntity = me;
            random = me.MyWorld.random;
        }

        public SteeringBehaviour(MovingEntity me, Random r)
        {
            movingEntity = me;
            random = r;
        }

        
    }

    
}
