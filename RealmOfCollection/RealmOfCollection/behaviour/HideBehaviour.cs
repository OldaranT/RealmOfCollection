using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;

namespace RealmOfCollection.behaviour
{
    public class HideBehaviour : SteeringBehaviour
    {
        public Vehicle hunter;
        public List<StaticEntity> objects;
        public HideBehaviour(MovingEntity me) : base(me)
        {
        }
        public HideBehaviour(MovingEntity me, Vehicle hunter, List<StaticEntity> objects) : base(me)
        {
            this.hunter = hunter;
            this.objects = objects;
        }

        public Vector2D Hide(Vehicle hunter, List<StaticEntity> Objects)
        {
            double DistToClosest = Double.MaxValue;
            Vector2D BestHidingSpot = new Vector2D();

            foreach(StaticEntity SE in Objects)
            {
                Vector2D HidingSpot = GetHidePosition(SE.Pos, SE.size.X, hunter.Pos);

                double dist = Vector2D.Vec2DDistanceSq(HidingSpot, ME.Pos);

                if(dist < DistToClosest)
                {
                    DistToClosest = dist;
                    BestHidingSpot = HidingSpot;
                    
                }
            }
            SteeringBehaviour ArriveB = new ArriveBehaviour(ME, BestHidingSpot, Deceleration.fast);

            return ArriveB.Calculate();
        }

        public Vector2D GetHidePosition(Vector2D PosOB, double radiusOB, Vector2D posHunter)
        {
            double DistanceFromBoundry = 30.0;
            Console.WriteLine("RadiusOB: " + radiusOB);
            double DistAway = radiusOB + DistanceFromBoundry;
            Vector2D WindFormAdjusment = new Vector2D(radiusOB/2, radiusOB/2);

            Vector2D ToOB = Vector2D.Vec2DNormalize(PosOB + WindFormAdjusment - posHunter);

            return (ToOB * DistAway) + PosOB;
        }

        public override Vector2D Calculate()
        {
            return this.Hide(hunter, objects);
        }
    }
}
