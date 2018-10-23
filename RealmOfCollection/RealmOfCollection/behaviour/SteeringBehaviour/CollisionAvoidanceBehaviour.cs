using RealmOfCollection.entity;
using RealmOfCollection.entity.MovingEntitys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.behaviour
{
    class CollisionAvoidanceBehaviour : SteeringBehaviour
    {
        Vector2D ahead { get; set; }
        Vector2D ahead2 { get; set; }

        public List<StaticEntity> objects;

        float MAX_SEE_AHEAD { get; set; }
        float MAX_AVOID_FORCE { get; set; }
        private Vector2D pos { get; set; }

        public CollisionAvoidanceBehaviour(MovingEntity me, float MaxLookRadius, List<StaticEntity> objects, float MaxForce) : base(me)
        {
            MAX_SEE_AHEAD = MaxLookRadius;
            this.objects = objects;
            MAX_AVOID_FORCE = MaxForce;
            pos = me.Pos;
            
        }

        private bool lineIntersectsCircle(StaticEntity obj)
        {
            bool intersects = false;
            //if (!(movingEntity is Hunter))
            //{
                if (obj.center.Distance(ahead) <= ((obj.size.Length() / 2) + (movingEntity.radius/2)) || obj.center.Distance(ahead2) <= ((obj.size.Length() / 2) + (movingEntity.radius/2)))
                {
                    intersects = true;
                }
            //}
            //else
            //{
            //    if (obj.center.Distance(movingEntity.Pos) <= ((obj.size.Length() / 2) + movingEntity.radius) || obj.center.Distance(ahead2) <= ((obj.size.Length() / 2) + movingEntity.radius))
            //    {
            //        intersects = true;
            //    }
            //}
            

            return intersects;
        }

        private StaticEntity findMostThreatening()
        {
            StaticEntity mostThreatening = null;

            foreach(StaticEntity s in objects)
            {
                bool collision = lineIntersectsCircle(s);

                if(collision && (mostThreatening == null || pos.Distance(s.Pos) < pos.Distance(mostThreatening.Pos)))
                {
                    mostThreatening = s;
                }
            }
            return mostThreatening;
        }

        public override Vector2D Calculate()
        {
            pos = movingEntity.Pos;
            ahead = movingEntity.Pos + Vector2D.Vec2DNormalize(movingEntity.Velocity) * MAX_SEE_AHEAD;
            ahead2 = movingEntity.Pos + Vector2D.Vec2DNormalize(movingEntity.Velocity) * MAX_SEE_AHEAD * 0.5;

            StaticEntity mostThreatening = findMostThreatening();
            Vector2D avoidance = new Vector2D();
            if (mostThreatening != null)
            {
                avoidance.X = ahead.X - mostThreatening.center.X;
                avoidance.Y = ahead.Y - mostThreatening.center.Y;

                avoidance = avoidance.Normalize();
                avoidance.ScaleBy(MAX_AVOID_FORCE);
            }

            return avoidance;

            //createAntenna();
            //Vector2D velocity = movingEntity.Velocity;

            //Vector2D force = new Vector2D();

            //detecting =  new bool[3]{ false, false, false };

            //foreach (StaticEntity obj in objects)
            //{
            //    double radius = obj.center.Clone().Sub(obj.Pos.Clone()).Length();
            //    radius += movingEntity.radius / 2;

            //    if (obj.center.Distance(antenna[0]) <= radius)
            //    {
            //        detecting[0] = true;
            //        force = velocity.PerpLeftHand();
            //        break;
            //    }
            //    if (obj.center.Distance(antenna[1]) <= radius)
            //    {
            //        detecting[1] = true;
            //        force = velocity.PerpRightHand();
            //        break;
            //    }
            //    if (obj.center.Distance(antenna[2]) <= radius)
            //    {
            //        detecting[2] = true;
            //        force = velocity.PerpLeftHand();
            //        break;
            //    }
            //}

            //if (detecting.Contains(true))
            //{
            //    force.Normalize().Multiply(MAX_AVOID_FORCE);
            //}
            //return force;
        }
    }
}
