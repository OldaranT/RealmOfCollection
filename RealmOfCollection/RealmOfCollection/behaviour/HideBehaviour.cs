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
                //Vector2D HidingSpot = GetHidePosition(SE.Pos, SE.size.Length(), hunter.Pos);
                Vector2D HidingSpot = GetHidePosition(SE.center, SE.size.Length(), hunter.Pos);

                double dist = Vector2D.Vec2DDistanceSq(HidingSpot, ME.Pos);

                if(dist < DistToClosest)
                {
                    DistToClosest = dist;
                    BestHidingSpot = HidingSpot;
                    
                }
            }
            if((hunter.Pos - ME.Pos).Length() > 500)
            {
                //Console.WriteLine("I Show you the wey");
                //WanderBehaviour WB = new WanderBehaviour(ME, 2500, 50, 0.001, ME.MyWorld.random);
                return new Vector2D(0, 0);
            }
            if(DistToClosest == Double.MaxValue)
            {
                //Console.WriteLine("I am out of here");
                EvadeBehaviour evade = new EvadeBehaviour(ME);
                return evade.Calculate();
            }
            else
            {
                //Console.WriteLine("HIDE HIDE HIDE HIDE!");
                SteeringBehaviour ArriveB = new ArriveBehaviour(ME, BestHidingSpot, Deceleration.fast);
                //Console.WriteLine(BestHidingSpot.ToString());

                return ArriveB.Calculate();
            }
        }

        public Vector2D GetHidePosition(Vector2D PosOB, double radiusOB, Vector2D posHunter)
        {
            double DistanceFromBoundry = 15.0;
            //Console.WriteLine("RadiusOB: " + radiusOB);
            double DistAway = radiusOB + DistanceFromBoundry;
            //Vector2D WindFormAdjusment = new Vector2D(radiusOB/2, radiusOB/2);

            //Vector2D ToOB = Vector2D.Vec2DNormalize(PosOB + WindFormAdjusment - posHunter);
            Vector2D ToOB = Vector2D.Vec2DNormalize(PosOB - posHunter);

            return (ToOB * DistAway) + PosOB;
        }

        public override Vector2D Calculate()
        {
            return this.Hide(hunter, objects);
        }
    }
}
