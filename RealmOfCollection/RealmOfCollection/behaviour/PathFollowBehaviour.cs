﻿using RealmOfCollection.entity;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.Graphs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.behaviour
{
    class PathFollowBehaviour : SteeringBehaviour
    {
        private SteeringBehaviour sb;
        public Path path;
        public bool arrived;

        public PathFollowBehaviour(MovingEntity me) : base(me)
        {
            sb = new SeekBehaviour(me);
            if(me is Hunter)
            {
                path = movingEntity.MyWorld.path;
            } else
            {
                path = new Path(movingEntity.MyWorld);
            }
        }

        public PathFollowBehaviour(MovingEntity me, Path path) : base(me)
        {
            sb = new SeekBehaviour(me);
            this.path = path;
        }

        public override Vector2D Calculate()
        {
            Vertex followPath = path.bestPath;

            if(followPath == null)
            {
                return new Vector2D();
            }

            if(followPath.adj.Count == 0 && movingEntity.Pos.DistanceSqrt(followPath.position) <= 100f)
            {
                movingEntity.Velocity = new Vector2D();
                return new Vector2D();
            }

            Vector2D force = sb.Calculate(followPath.position);

            followPath = setNextTarget(followPath);

            path.bestPath = followPath;

            return force;

        }

        public Vertex setNextTarget(Vertex currentTarget)
        {
            float distance = movingEntity.Pos.DistanceSqrt(currentTarget.position);

            if(distance < 100)
            {
                if(currentTarget.adj.Count > 0)
                {
                    currentTarget = currentTarget.adj[0].destination;
                } else if (!arrived)
                {
                    arrived = true;
                    sb = new SeekBehaviour(movingEntity);
                }
            }
            return currentTarget;
        }

        public override void Draw(Graphics g)
        {
            path.Render(g);
        }
    }
}
