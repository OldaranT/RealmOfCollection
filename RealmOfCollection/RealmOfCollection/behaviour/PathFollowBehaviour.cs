using RealmOfCollection.entity;
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
            path = new Path(me.MyWorld);
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

            if(followPath.adj.Count == 0 && sb is SeekBehaviour)
            {
                sb = new ArriveBehaviour(ME, followPath.position);
            }

            Vector2D force = sb.Calculate();

            setNextTarget(ref followPath);

            path.bestPath = followPath;

            return force;

        }

        public void setNextTarget(ref Vertex currentTarget)
        {
            float distance = ME.Pos.DistanceSqrt(currentTarget.position);

            if(distance < 100)
            {
                if(currentTarget.adj.Count > 0)
                {
                    currentTarget = currentTarget.adj[0].destination;
                } else if (!arrived)
                {
                    arrived = true;
                    sb = new SeekBehaviour(ME);
                }
            }

        }

        public override void Draw(Graphics g)
        {
            path.Draw(g);
        }
    }
}
