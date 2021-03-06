﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;
using RealmOfCollection.entity.MovingEntitys;

namespace RealmOfCollection.behaviour
{
    public class HideBehaviour : SteeringBehaviour
    {
        public Hunter hunter;
        public List<StaticEntity> objects;
        private int hideDistance;
        public HideBehaviour() : base() { }
        public HideBehaviour(MovingEntity me, Hunter hunter, List<StaticEntity> objects, int hideDistance) : base(me)
        {
            this.hunter = hunter;
            this.hideDistance = hideDistance;
            this.objects = objects;
        }

        public Vector2D Hide(Hunter hunter, List<StaticEntity> Objects)
        {
            double DistToClosest = Double.MaxValue;
            Vector2D BestHidingSpot = new Vector2D();
            try
            {
                foreach (StaticEntity SE in Objects)
                {
                    Vector2D HidingSpot = GetHidePosition(SE.center, SE.size.Length(), hunter.Pos);

                    double dist = Vector2D.Vec2DDistanceSq(HidingSpot, movingEntity.Pos);

                    if (dist < DistToClosest)
                    {
                        DistToClosest = dist;
                        BestHidingSpot = HidingSpot;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("HidingBehavior: " + e.Message);
            }
            if ((hunter.Pos - movingEntity.Pos).Length() > hideDistance)
            {
                return new Vector2D(0, 0);
            }
            if (DistToClosest == Double.MaxValue)
            {
                EvadeBehaviour evade = new EvadeBehaviour(movingEntity);
                return evade.Calculate();
            }
            else
            {
                SteeringBehaviour ArriveB = new ArriveBehaviour(movingEntity, BestHidingSpot, Deceleration.fast);
                return ArriveB.Calculate();
            }
        }

        public Vector2D GetHidePosition(Vector2D PosOB, double radiusOB, Vector2D posHunter)
        {
            double DistanceFromBoundry = 15.0;
            double DistAway = radiusOB + DistanceFromBoundry;

            Vector2D ToOB = Vector2D.Vec2DNormalize(PosOB - posHunter);

            return (ToOB * DistAway) + PosOB;
        }

        public override Vector2D Calculate()
        {
            return this.Hide(hunter, objects);
        }
    }
}
