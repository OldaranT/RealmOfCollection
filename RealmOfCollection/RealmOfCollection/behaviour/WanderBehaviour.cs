using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;

namespace RealmOfCollection.behaviour
{
    //class WanderBehaviour : SteeringBehaviour
    //{

    //    double wanderRadius;
    //    double wanderDistance;
    //    double wanderJitter;
    //    Vector2D wanderTarget;

    //    public WanderBehaviour(MovingEntity me, double radius, double distance, double jitter) : base(me)
    //    {
    //        wanderRadius = radius;
    //        wanderDistance = distance;
    //        wanderJitter = jitter;
    //        float theta = randomFloat();
    //        wanderTarget = new Vector2D(wanderRadius * Math.Cos(theta), wanderRadius * Math.Sin(theta));
    //    }

    //    public double randomDouble()
    //    {
    //        Random r = new Random();
    //        double next = r.NextDouble() * 2.0 - 1.0;
    //        Console.WriteLine(next);
    //        return next;
    //    }

    //    public float randomFloat()
    //    {
    //        Random r = new Random();
    //        return NextFloat(r);
    //    }

    //    static float NextFloat(Random random)
    //    {
    //        double mantissa = (random.NextDouble() * 2.0) - 1.0;
    //        double exponent = Math.Pow(2.0, random.Next(-126, 128));
    //        return (float)(mantissa * exponent);
    //    }

    //    public override Vector2D Calculate()
    //    {
    //        wanderTarget.Add(new Vector2D(randomDouble() * wanderJitter, randomDouble() * wanderJitter));
    //        wanderTarget = wanderTarget.Normalize();
    //        wanderTarget = wanderTarget.Multiply(wanderRadius);
    //        Vector2D targetLocal = wanderTarget.Add(new Vector2D(wanderDistance, 0));
    //        Vector2D targetWorld = Vector2D.PointToWorldSpace(targetLocal, ME.Heading, ME.Side, ME.Pos);


            
    //        return (targetWorld.Sub(ME.Pos));
    //    }
    //}
}
