﻿using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.behaviour
{
    class EntityAvoidanceBehaviour : SteeringBehaviour
    {
        List<MovingEntity> entities { get; set; }

        float MAX_AVOID_FORCE { get; set; }

        private Vector2D pos { get; set; }

        public EntityAvoidanceBehaviour() : base() { }

        public EntityAvoidanceBehaviour(MovingEntity me, List<MovingEntity> entities) : base(me)
        {
            this.entities = entities;
            MAX_AVOID_FORCE = 25;
        }

        private bool intersects(MovingEntity m)
        {
            bool intersects = false;
            if (m.Pos.Distance(movingEntity.Pos) <= (m.radius + movingEntity.radius))
            {
                intersects = true;
            }
            return intersects;
        }

        private MovingEntity findMostThreatening()
        {
            MovingEntity mostThreatening = null;

            foreach (MovingEntity m in entities)
            {
                if (m == movingEntity)
                {
                    continue;
                }
                else
                {
                    bool collision = intersects(m);

                    if (collision && (mostThreatening == null || pos.Distance(m.Pos) < pos.Distance(mostThreatening.Pos)))
                    {
                        mostThreatening = m;
                    }
                }
            }
            return mostThreatening;
        }

        public override Vector2D Calculate()
        {
            pos = movingEntity.Pos;

            MovingEntity mostThreatening = findMostThreatening();
            Vector2D avoidance = new Vector2D(0, 0);

            if (mostThreatening != null)
            {
                avoidance.X = pos.X - mostThreatening.Pos.X;
                avoidance.Y = pos.Y - mostThreatening.Pos.Y;

                avoidance = avoidance.Normalize();
                avoidance.ScaleBy(MAX_AVOID_FORCE);
            }
            else
            {
                // nullify the avoidance force
                avoidance.ScaleBy(0);
            }

            return avoidance;
        }
    }
}
