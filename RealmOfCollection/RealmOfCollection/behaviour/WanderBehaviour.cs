using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;

namespace RealmOfCollection.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        Vector2D wanderTarget;
        double wanderRadius { get; set; }
        double wanderDistance { get; set; }
        double wanderJitter { get; set; }
        Vector2D TargetLocation;

        public WanderBehaviour(MovingEntity me, double radius, double distance, double jitter, Random r) : base(me, r)
        {
            wanderRadius = radius;
            wanderDistance = distance;
            wanderJitter = jitter;
            float theta = randomFloat();
            wanderTarget = new Vector2D(wanderRadius * Math.Cos(theta), wanderRadius * Math.Sin(theta));
        }

        public double randomDouble()
        {
            double next = random.NextDouble() - random.NextDouble();
            return next;
        }

        public float randomFloat()
        {
            return NextFloat(random);
        }

        public float NextFloat(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }

        public override Vector2D Calculate()
        {
            Vector2D MyPos = ME.Pos;
            wanderTarget.Add(new Vector2D(randomDouble() * wanderJitter, randomDouble() * wanderJitter));
            wanderTarget = wanderTarget.Normalize();
            wanderTarget = wanderTarget.Multiply(wanderRadius);
            Vector2D targetLocal = wanderTarget.Add(new Vector2D(wanderDistance, 0));
            

            Vector2D targetWorld = Vector2D.PointToWorldSpace(targetLocal, ME.Heading, ME.Side, ME.Pos);
            targetWorld = targetWorld.Sub(ME.Pos);
            TargetLocation = targetWorld;
            float MaxSpeed = ME.MaxSpeed;

            Vector2D DesiredVelocity = new Vector2D((targetWorld.X - MyPos.X), (targetWorld.Y - MyPos.Y));
            DesiredVelocity = DesiredVelocity.Normalize();
            DesiredVelocity = DesiredVelocity.Multiply(MaxSpeed);
            DesiredVelocity = DesiredVelocity.Sub(ME.Velocity);
            return DesiredVelocity;
        }
    }
}
