using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection
{
    abstract class SteeringBehaviour
    {
        public MovingEntity ME { get; set; }
        public abstract Vector2D Calculate();
        double wanderRadius = 300;
        double wanderDistance = 2;
        double wanderJitter = 20;
        Vector2D wanderTarget = new Vector2D(100,100);

        public SteeringBehaviour(MovingEntity me)
        {
            ME = me;
            double theta = randomFloat()*(2 * Math.PI);

            wanderTarget = new Vector2D(wanderRadius * Math.Cos(theta), wanderRadius * Math.Sin(theta));
        }

        public double randomDouble()
        {
            DateTime currentDate = DateTime.Now;
            Random r = new Random(currentDate.Millisecond);
            double next = r.NextDouble() * 2.0 - 1.0;
            Console.WriteLine(next);
            return next;
        }

        public float randomFloat()
        {
            Random r = new Random();
            return NextFloat(r);
        }

        public float NextFloat(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

        public Vector2D wanderT()
        {
            wanderTarget.Add(new Vector2D(randomDouble() * wanderJitter, randomDouble() * wanderJitter));
            wanderTarget = wanderTarget.Normalize();
            wanderTarget = wanderTarget.Multiply(wanderRadius);
            Vector2D targetLocal = wanderTarget.Add(new Vector2D(wanderDistance, 0));
            Vector2D targetWorld = Vector2D.PointToWorldSpace(targetLocal, ME.Heading, ME.Side, ME.Pos);



            return (targetWorld.Sub(ME.Pos));
        }
    }

    
}
